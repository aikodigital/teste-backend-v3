using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Swashbuckle.AspNetCore.Swagger;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers(); // Adiciona suporte para controllers
        services.AddSwaggerGen();  // Adiciona suporte para Swagger
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseSwagger(); // Habilita o Swagger
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
        }

        app.UseHttpsRedirection();
        app.UseRouting(); // Adiciona suporte ao roteamento
        app.UseAuthorization(); // Adiciona suporte à autorização

        // Configure os endpoints de API
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers(); // Mapeia os controllers
        });
    }
}
