using Microsoft.OpenApi.Models;
using Scalar.AspNetCore;

namespace CacioPepe.Rest.Modules;

public class OpenApiModule : IModule
{
    public bool IsEnabled => true;
    public int Order => 0;

    public IServiceCollection Register(WebApplicationBuilder builder)
    {
        builder.Services.AddOpenApi(options =>
        {
            options.AddDocumentTransformer((document, _, _) =>
            {
                document.Servers = [new OpenApiServer {Url = "/"}];
                document.Info = new OpenApiInfo
                {
                    Title = "CacioPepe API v1",
                    Version = "v1.0",
                    Description = "CacioPepe API for managing spiedo bresciano preparation",
                    Contact = new OpenApiContact
                    {
                        Name = "CacioPepe"
                    }
                };

                return Task.CompletedTask;
            });
        });

        return builder.Services;
    }

    public WebApplication Configure(WebApplication app)
    {
        app.MapOpenApi();
        app.MapScalarApiReference(options =>
        {
            options.WithTitle("CacioPepe API v1")
                .WithTheme(ScalarTheme.None);
        });

        return app;
    }
}