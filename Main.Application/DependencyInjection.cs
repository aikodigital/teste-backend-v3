using Main.Application.Services.StatementPrinter;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Main.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<StatementPrinterService>();
            services.AddScoped<XmlStatementPrinterService>();
            services.AddScoped<IStatementPrinterService>(provider =>
            {
                var httpContextAccessor = provider.GetService<IHttpContextAccessor>();

                string? header = httpContextAccessor?.HttpContext?.Request?.Headers?["X-ResponseFormat"];

                return header switch
                {
                    "xml" => provider.GetRequiredService<XmlStatementPrinterService>(),
                    _ => provider.GetRequiredService<StatementPrinterService>(),
                };
            });
            return services;
        }
    }
}
