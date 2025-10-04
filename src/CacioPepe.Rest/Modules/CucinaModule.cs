using CacioPepe.Cucina.Facade;
using CacioPepe.Cucina.Facade.Endpoints;

namespace CacioPepe.Rest.Modules;

public class CucinaModule : IModule
{
    public bool IsEnabled => true;
    public int Order => 0;
    
    public IServiceCollection Register(WebApplicationBuilder builder)
    {
        builder.Services.AddCucinaFacade();
        
        return builder.Services;
    }

    public WebApplication Configure(WebApplication app)
    {
        app.MapCucinaEndpoints();
        
        return app;
    }
}