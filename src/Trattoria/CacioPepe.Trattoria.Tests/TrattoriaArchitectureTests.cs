using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using CacioPepe.Trattoria.Facade;
using NetArchTest.Rules;

namespace CacioPepe.Trattoria.Tests;

[ExcludeFromCodeCoverage]
public class TrattoriaArchitectureTests
{
    [Fact]
    public void Should_TrattoriaArchitecture_BeCompliant()
    {
        var types = Types.InAssembly(typeof(TrattoriaFacadeHelper).Assembly);

        var forbiddenAssemblies = new List<string>
        {
            "CacioPepe.Cucina.Domain",
            "CacioPepe.Cucina.Facade",
            "CacioPepe.Cucina.Infrastructure",
            "CacioPepe.Cucina.ReadModel",
            "CacioPepe.Cucina.SharedKernel"
        };
        
        var result = types
            .ShouldNot()
            .HaveDependencyOnAny(forbiddenAssemblies.ToArray())
            .GetResult()
            .IsSuccessful;

        Assert.True(result);
    }
    
    [Fact]
    public void TrattoriaProjects_Should_Having_Namespace_StartingWith_Trattoria()
    {
        var trattoriaModulePath = Path.Combine(VisualStudioProvider.TryGetSolutionDirectoryInfo().FullName, "Trattoria");
        var subFolders = Directory.GetDirectories(trattoriaModulePath);

        var netVersion = Environment.Version;

        var trattoriaAssemblies = (from folder in subFolders
            let binFolder = Path.Join(folder, "bin", "Debug", $"net{netVersion.Major}.{netVersion.Minor}")
            where Directory.Exists(binFolder)
            let files = Directory.GetFiles(binFolder)
            let folderArray = folder.Split(Path.DirectorySeparatorChar)
            select files.FirstOrDefault(f => f.EndsWith($"{folderArray[folderArray!.Length - 1]}.dll"))
            into assemblyFilename
            where assemblyFilename != null && !assemblyFilename.Contains("Test")
            select Assembly.LoadFile(assemblyFilename)).ToList();
        
        var trattoriaTypes = Types.InAssemblies(trattoriaAssemblies)
            .That()
            .DoNotHaveNameStartingWith("<>")
            .And()
            .AreNotNested()
            .GetTypes();
        
        var typesWithCorrectNamespace = Types.InAssemblies(trattoriaAssemblies)
            .That()
            .ResideInNamespaceStartingWith("CacioPepe.Trattoria")
            .And()
            .AreNotNested()
            .GetTypes();
        
        var trattoriaTypeArray = trattoriaTypes as Type[] ?? trattoriaTypes.ToArray();
        var typesWithIncorrectNamespace = trattoriaTypeArray.Except(typesWithCorrectNamespace).ToList();

        foreach (var type in typesWithIncorrectNamespace)
        {
            if (type.Namespace != null)
                Assert.Fail(
                    $"Namespace violation detected: {type.FullName} in assembly {type.Assembly.GetName().Name} should start " +
                    $"with 'CacioPepe.Trattoria' but is in namespace '{type.Namespace}'");
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