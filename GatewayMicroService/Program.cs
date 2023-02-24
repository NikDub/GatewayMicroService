using Ocelot.Middleware;

namespace GatewayMicroService;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);


        builder.Services.AddControllers();
        builder.Services.ConfigureOcelot(builder.Configuration);
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddCors();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();
        app.UseCors(x => x.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseOcelot().Wait();

        app.MapControllers();

        app.Run();
    }
}