using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace GatewayMicroService
{
    public static class ServiceExstensions
    {
        public static void ConfigureJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    options.Authority = configuration.GetValue<string>("Routes:AuthorityRoute") ??
                                        throw new NotImplementedException();
                    options.Audience = configuration.GetValue<string>("Routes:Scopes") ??
                                       throw new NotImplementedException();
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = true,
                        ValidAudience = configuration.GetValue<string>("Routes:Scopes") ??
                                        throw new NotImplementedException(),
                        ValidateIssuer = true,
                        ValidIssuer = configuration.GetValue<string>("Routes:AuthorityRoute") ??
                                      throw new NotImplementedException(),
                        ValidateLifetime = true
                    };
                });
        }

        public static void ConfigureCors(this IServiceCollection services)
            => services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                    builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
            });
    }
}
