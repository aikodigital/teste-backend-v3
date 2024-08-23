#region

using Microsoft.EntityFrameworkCore;
using TheatricalPlayersRefactoringKata.API.Repositories;
using TheatricalPlayersRefactoringKata.API.Repositories.Interfaces;

#endregion

namespace TheatricalPlayersRefactoringKata.API.Data;

public static class MigrationExtensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<ApiDbContext>(options =>
                options.UseNpgsql(config.GetConnectionString("DefaultConnection")))
            .AddScoped<IPlayRepository, PlayRepository>()
            .AddScoped<IPerformanceRepository, PerfRepository>()
            .AddScoped<IInvoiceRepository, InvoiceRepository>();
        return services;
    }
}