using Microsoft.Extensions.DependencyInjection;

namespace CacioPepe.Cucina.Domain;

public static class CucinaDomainHelper
{
    public static IServiceCollection AddCucinaDomain(this IServiceCollection services)
    {
        // Register domain services here
        return services;
    }
}