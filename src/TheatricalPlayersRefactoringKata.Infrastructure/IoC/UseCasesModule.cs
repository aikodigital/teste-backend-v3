using Microsoft.Extensions.DependencyInjection;
using TheatricalPlayersRefactoringKata.Application.UseCase.Statement;
using TheatricalPlayersRefactoringKata.Application.UseCase.Statement.Create;
using TheatricalPlayersRefactoringKata.Application.UseCase.Statement.Print;

namespace TheatricalPlayersRefactoringKata.Infrastructure.IoC;

public static class UseCasesModule
{
    public static void AddUseCases(this IServiceCollection services)
    {
        services.AddScoped<ICreateStatementUseCase, CreateStatementUseCase>();
        services.AddScoped<IPrintStatementUseCase, PrintStatementUseCase>();
    }
}