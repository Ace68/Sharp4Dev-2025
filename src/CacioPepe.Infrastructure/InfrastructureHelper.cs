using Microsoft.Extensions.DependencyInjection;

namespace CacioPepe.Infrastructure;

public static class InfrastructureHelper
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        // Register shared infrastructure services here
        return services;
    }
}