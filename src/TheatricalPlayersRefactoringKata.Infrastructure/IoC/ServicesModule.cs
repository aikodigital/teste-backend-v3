using Microsoft.Extensions.DependencyInjection;
using TheatricalPlayersRefactoringKata.Application.Services;
using TheatricalPlayersRefactoringKata.Infrastructure.Services;

namespace TheatricalPlayersRefactoringKata.Infrastructure.IoC;

public static class ServicesModule
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddSingleton<IStatementService, StatementService>();
    }
}