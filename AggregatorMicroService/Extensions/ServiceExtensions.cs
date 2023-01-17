using AggregatorMicroService.Middlewares;
using AggregatorMicroService.Service;
using AggregatorMicroService.Service.Abstraction;
using AggregatorMicroService.Static;
using Microsoft.OpenApi.Models;

namespace AggregatorMicroService.Extensions;

public static class ServiceExtensions
{
    public static void ConfigureServices(this IServiceCollection services)
    {
        services.AddScoped<IAggregatorService, AggregatorService>();

        services.AddScoped<IHttpService, HttpService>();

        services.AddSingleton<UrlPath>();
        services.AddAutoMapper(typeof(MappingProfile));
        services.AddHttpClient();
    }

    public static void ConfigureSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(setup =>
        {
            setup.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Place to add JWT with Bearer",
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });

            setup.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                        Name = "Bearer"
                    },
                    new List<string>()
                }
            });
        });
    }

    public static void ConfigureExceptionHandler(this IApplicationBuilder app)
    {
        app.UseMiddleware<ExceptionHandlingMiddleware>();
    }
}