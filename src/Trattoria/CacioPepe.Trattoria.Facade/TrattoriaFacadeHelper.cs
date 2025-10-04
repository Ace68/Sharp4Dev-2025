using CacioPepe.Trattoria.Domain;
using CacioPepe.Trattoria.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace CacioPepe.Trattoria.Facade;

public static class TrattoriaFacadeHelper
{
    public static IServiceCollection AddTrattoriaFacade(this IServiceCollection services)
    {
        services.AddTrattoriaDomain();
        services.AddTrattoriaInfrastructure();

        return services;
    }
}