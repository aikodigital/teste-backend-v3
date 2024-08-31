using Main.Application.Services.Authentication;
using Main.Application.Services.StatementPrinter;
using Microsoft.Extensions.DependencyInjection;

namespace Main.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IStatementPrinterService, StatementPrinterService>();
            return services;
        }
    }
}
