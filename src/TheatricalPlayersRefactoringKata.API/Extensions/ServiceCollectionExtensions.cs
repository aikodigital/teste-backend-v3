using TheatricalPlayersRefactoringKata.Application.Services;
using TheatricalPlayersRefactoringKata.Application.Services.Interfaces;
using TheatricalPlayersRefactoringKata.Infrastructure.Data;
using TheatricalPlayersRefactoringKata.Infrastructure.Repositories;
using TheatricalPlayersRefactoringKata.Infrastructure.Repositories.Interfaces;
using TheatricalPlayersRefactoringKata.Infrastructure.Strategies.Exports;

namespace TheatricalPlayersRefactoringKata.API.Extensions;

/// <summary>
/// Extension methods for adding services to the IServiceCollection.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds invoice processor services to the IServiceCollection.
    /// </summary>
    /// <param name="services">The IServiceCollection to add services to.</param>
    /// <returns>The IServiceCollection with the added services.</returns>
    public static IServiceCollection AddStatementProcessorServices(this IServiceCollection services)
    {
        // Register the RabbitMQ statement
        // consumer and producer as singleton services.
        services.AddSingleton<StatementProducer>();
        services.AddHostedService<StatementConsumer>();

        // Register the export strategies as singleton services.
        services.AddSingleton<IStatementExportStrategy, XmlStatementExportStrategy>();
        services.AddSingleton<IStatementExportStrategy, TxtStatementExportStrategy>();
        return services;
    }

    /// <summary>
    /// Adds repository services to the IServiceCollection.
    /// </summary>
    /// <param name="services">The IServiceCollection to add services to.</param>
    /// <returns>The IServiceCollection with the added services.</returns>
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        // Unit of work is registered as a scoped service.
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        // Register the repository as a scoped service.
        services.AddScoped<IInvoiceRepository, InvoiceRepository>();
        return services;
    }

    /// <summary>
    /// Adds services to the IServiceCollection.
    /// </summary>
    /// <param name="services">The IServiceCollection to add services to.</param>
    /// <returns>The IServiceCollection with the added services.</returns>
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        // Register the InvoiceService as a scoped service.
        services.AddScoped<IInvoiceService, InvoiceService>();
        return services;
    }
}