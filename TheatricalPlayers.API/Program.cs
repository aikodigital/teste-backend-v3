using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace TheatricalPlayers.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddAuthorization();

        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "TheatricalPlayers API", Version = "v1" });

            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            c.IncludeXmlComments(xmlPath);
        });

        builder.Services.AddControllers();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseAuthorization();
        
        app.UseRouting();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
        
        UseSwaggerConfiguration(app);

        app.Run();
    }
    
    public static void UseSwaggerConfiguration(IApplicationBuilder app)
    {
        app.UseSwagger(c => { c.RouteTemplate = "api/theatricalplayers/{documentName}/swagger/swagger.json"; });

        app.UseSwaggerUI(c =>
        {
            c.RoutePrefix = "api/theatricalplayers/v1/swagger";
            c.SwaggerEndpoint("swagger.json", "Theatrical Players API V1 ....");
        });
    }
}

