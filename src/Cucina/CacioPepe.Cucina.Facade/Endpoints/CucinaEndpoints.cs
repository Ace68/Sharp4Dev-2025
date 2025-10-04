using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace CacioPepe.Cucina.Facade.Endpoints;

public static class CucinaEndpoints
{
    public static IEndpointRouteBuilder MapCucinaEndpoints(this IEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup("v1/cucina")
            .WithTags("Cucina");

        group.MapGet("/", () => Results.Ok("Cucina module is working"))
            .WithName("GetCucinaStatus")
            .WithSummary("Get Cucina module status");

        return endpoints;
    }
}