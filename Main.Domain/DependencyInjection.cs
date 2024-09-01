using Main.Domain.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Main.Domain
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDomain(this IServiceCollection services)
        {
            services.AddTransient<StatementFormatter>();
            services.AddTransient<StatementCalculator>();
            return services;
        }
    }
}
