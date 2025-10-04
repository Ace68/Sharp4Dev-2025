using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace CacioPepe.Trattoria.Facade.Endpoints;

public static class TrattoriaEndpoints
{
    public static IEndpointRouteBuilder MapTrattoriaEndpoints(this IEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup("v1/trattoria")
            .WithTags("Trattoria");

        group.MapGet("/", () => Results.Ok("Trattoria module is working"))
            .WithName("GetTrattoriaStatus")
            .WithSummary("Get Trattoria module status");

        return endpoints;
    }
}