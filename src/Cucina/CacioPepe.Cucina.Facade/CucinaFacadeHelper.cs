using CacioPepe.Cucina.Domain;
using CacioPepe.Cucina.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace CacioPepe.Cucina.Facade;

public static class CucinaFacadeHelper
{
    public static IServiceCollection AddCucinaFacade(this IServiceCollection services)
    {
        services.AddCucinaDomain();
        services.AddCucinaInfrastructure();

        return services;
    }
}