using Microsoft.Extensions.DependencyInjection;

namespace CacioPepe.Trattoria.Domain;

public static class TrattoriaDomainHelper
{
    public static IServiceCollection AddTrattoriaDomain(this IServiceCollection services)
    {
        // Register domain services here
        return services;
    }
}