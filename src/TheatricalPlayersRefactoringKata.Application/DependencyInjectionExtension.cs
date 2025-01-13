using Microsoft.Extensions.DependencyInjection;
using TheatricalPlayersRefactoringKata.Application.Services.StatementManipulate;
using TheatricalPlayersRefactoringKata.Application.UseCases.Statements.Print;

namespace TheatricalPlayersRefactoringKata.Application
{
    public static class DependencyInjectionExtension
    {
        public static void AddApplication(this IServiceCollection services)
        {
            AddServices(services);
        }

        public static void AddServices(IServiceCollection services)
        {
            services.AddScoped<IPrintStatementUseCase, PrintStatementUseCase>();
            services.AddScoped<IStatementPrinterService, StatementPrinterService>();
        }
    }
}
