using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using System;
using TheatricalPlayersRefactoringKata;
using Microsoft.AspNetCore.Hosting;
using TheatricalPlayersRefactoringKata.Application.Adapters;
using TheatricalPlayersRefactoringKata.Application.Interfaces;
using TheatricalPlayersRefactoringKata.Application.Services;
using TheatricalPlayersRefactoringKata.Application.Strategies;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        // DATABASE
        services.AddSqlite<ApplicationDbContext>("Data Source=./TheatricalPlayersRefactoringKata.db");

        // UTILITIES
        services.AddAutoMapper(typeof(Program));

        // DI
        services.AddSingleton<IFormatterAdapter, XmlFormatterAdapter>();
        services.AddSingleton<CalculationStrategyFactory>();
        services.AddSingleton<IStatementPrinterService, StatementPrinterService>();

        // BACKGROUND SERVICES
        services.AddSingleton<StatementProcessingService>();
        services.AddSingleton<IHostedService, StatementProcessingService>(
                           serviceProvider => serviceProvider.GetService<StatementProcessingService>());

        // SWAGGER
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(o => o.InferSecuritySchemes());

        return services;
    }

    public static async Task InitialiseDatabaseAsync(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var services = scope.ServiceProvider;

        try
        {
            var context = services.GetRequiredService<ApplicationDbContext>();
            await context.Database.MigrateAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while migrating the database: {ex.Message}");
        }
    }
}
