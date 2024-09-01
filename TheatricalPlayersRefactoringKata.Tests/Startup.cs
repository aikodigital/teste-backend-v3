using Main.Application.Services.StatementPrinter;
using Microsoft.Extensions.DependencyInjection;
using Main.Domain.Services;
using System;

namespace TheatricalPlayersRefactoringKata.Tests
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<StatementFormatter>();
            services.AddTransient<StatementCalculator>();
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
        }
    }
}
