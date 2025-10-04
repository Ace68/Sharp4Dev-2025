using Microsoft.Extensions.DependencyInjection;

namespace CacioPepe.Cucina.Infrastructure;

public static class CucinaInfrastructureHelper
{
    public static IServiceCollection AddCucinaInfrastructure(this IServiceCollection services)
    {
        // Register infrastructure services here
        return services;
    }
}