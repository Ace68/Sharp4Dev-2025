using Microsoft.Extensions.DependencyInjection;

namespace CacioPepe.Trattoria.Infrastructure;

public static class TrattoriaInfrastructureHelper
{
    public static IServiceCollection AddTrattoriaInfrastructure(this IServiceCollection services)
    {
        // Register infrastructure services here
        return services;
    }
}