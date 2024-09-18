using Microsoft.OpenApi.Models;
using TheatricalPlayersRefactoringKata.Injection;

namespace TheatricalPlayersRefactoringKata;

public class Program
{
    public static void Main(string[] args)
    {
        var consoleLogger = LoggerFactory.Create(b => b.AddConsole());

        var builder = WebApplication.CreateBuilder(args);

        builder.Services.StartInjection();

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();

        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1",new OpenApiInfo
            {
                Title = "TheatricalPlayersRefactoringKata - V1",
                Version = "v1"
            });

            var filePath = Path.Combine(AppContext.BaseDirectory, "TheatricalPlayersRefactoringKata.xml");
            c.IncludeXmlComments(filePath);
        });

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();

        app.Run();
    }
}