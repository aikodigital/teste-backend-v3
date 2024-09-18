using Microsoft.Extensions.DependencyInjection;
using TheatricalPlayersRefactoringKata.Application.Interfaces;
using TheatricalPlayersRefactoringKata.Application.Services;
using TheatricalPlayersRefactoringKata.Domain.Interface;
using TheatricalPlayersRefactoringKata.Infra;
using TheatricalPlayersRefactoringKata.Infra.Repositories;

namespace TheatricalPlayersRefactoringKata.Injection;

public static class ServiceInjection
{
    public static void StartInjection(this IServiceCollection services)
    {
        MongoDbMapping.Configure();
        services.AddScoped<MongoContext>();

        services.AddScoped<IPlayRepository, PlayRepository>();
        services.AddScoped<IPlayService, PlayService>();
        services.AddScoped<IInvoiceRepository, InvoiceRepository>();
        services.AddScoped<IInvoiceService, InvoiceService>();
    }
}