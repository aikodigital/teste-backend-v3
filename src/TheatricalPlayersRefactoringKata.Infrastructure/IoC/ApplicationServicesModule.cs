using Microsoft.Extensions.DependencyInjection;
using TheatricalPlayersRefactoringKata.Application.Services;
using TheatricalPlayersRefactoringKata.Infrastructure.Services;

namespace TheatricalPlayersRefactoringKata.Infrastructure.IoC;

public static class ApplicationServicesModule
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddSingleton<IStatementPrinterService, StatementPrinterService>();
    }
}