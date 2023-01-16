using Ocelot.DependencyInjection;

namespace GatewayMicroService;

public static class ServiceExtensions
{
    public static void ConfigureOcelot(this IServiceCollection services, ConfigurationManager builderConfiguration)
    {
        builderConfiguration
            .AddJsonFile("Ocelot.json", false, true);


        services.AddOcelot(builderConfiguration);
    }
}