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
            services.AddScoped<StatementPrinterService>();
            services.AddScoped<XmlStatementPrinterService>();
            services.AddScoped<Func<string, IStatementPrinterService>>(provider => key =>
            {
                return key switch
                {
                    "xml" => provider.GetRequiredService<XmlStatementPrinterService>(),
                    _ => provider.GetRequiredService<StatementPrinterService>(),
                };
            });
            return services;
        }
    }
}
