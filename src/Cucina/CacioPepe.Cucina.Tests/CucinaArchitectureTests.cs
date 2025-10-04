using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using CacioPepe.Cucina.Facade;
using NetArchTest.Rules;

namespace CacioPepe.Cucina.Tests;

[ExcludeFromCodeCoverage]
public class CucinaArchitectureTests
{
    [Fact]
    public void Should_CucinaArchitecture_BeCompliant()
    {
        var types = Types.InAssembly(typeof(CucinaFacadeHelper).Assembly);

        var forbiddenAssemblies = new List<string>
        {
            "CacioPepe.Trattoria.Domain",
            "CacioPepe.Trattoria.Facade",
            "CacioPepe.Trattoria.Infrastructure",
            "CacioPepe.Trattoria.ReadModel",
            "CacioPepe.Trattoria.SharedKernel"
        };
        
        var result = types
            .ShouldNot()
            .HaveDependencyOnAny(forbiddenAssemblies.ToArray())
            .GetResult()
            .IsSuccessful;

        Assert.True(result);
    }
    
    [Fact]
    public void CucinaProjects_Should_Having_Namespace_StartingWith_Cucina()
    {
        var cucinaModulePath = Path.Combine(VisualStudioProvider.TryGetSolutionDirectoryInfo().FullName, "Cucina");
        var subFolders = Directory.GetDirectories(cucinaModulePath);

        var netVersion = Environment.Version;

        var cucinaAssemblies = (from folder in subFolders
            let binFolder = Path.Join(folder, "bin", "Debug", $"net{netVersion.Major}.{netVersion.Minor}")
            where Directory.Exists(binFolder)
            let files = Directory.GetFiles(binFolder)
            let folderArray = folder.Split(Path.DirectorySeparatorChar)
            select files.FirstOrDefault(f => f.EndsWith($"{folderArray[folderArray!.Length - 1]}.dll"))
            into assemblyFilename
            where assemblyFilename != null && !assemblyFilename.Contains("Test")
            select Assembly.LoadFile(assemblyFilename)).ToList();
        
        var cucinaTypes = Types.InAssemblies(cucinaAssemblies)
            .That()
            .DoNotHaveNameStartingWith("<>")
            .And()
            .AreNotNested()
            .GetTypes();
        
        var typesWithCorrectNamespace = Types.InAssemblies(cucinaAssemblies)
            .That()
            .ResideInNamespaceStartingWith("CacioPepe.Cucina")
            .And()
            .AreNotNested()
            .GetTypes();
        
        var cucinaTypeArray = cucinaTypes as Type[] ?? cucinaTypes.ToArray();
        var typesWithIncorrectNamespace = cucinaTypeArray.Except(typesWithCorrectNamespace).ToList();

        foreach (var type in typesWithIncorrectNamespace)
        {
            if (type.Namespace != null)
                Assert.Fail(
                    $"Namespace violation detected: {type.FullName} in assembly {type.Assembly.GetName().Name} should start " +
                    $"with 'CacioPepe.Cucina' but is in namespace '{type.Namespace}'");
        }
    }
    
    private static class VisualStudioProvider
    {
        public static DirectoryInfo TryGetSolutionDirectoryInfo(string? currentPath = null)
        {
            var directory = new DirectoryInfo(
                currentPath ?? Directory.GetCurrentDirectory());
            while (directory != null && !directory.GetFiles("*.sln").Any())
            {
                directory = directory.Parent;
            }
            return directory!
                   ?? throw new DirectoryNotFoundException("Solution directory not found. Make sure to run this test from a solution folder.");
        }
    }
}