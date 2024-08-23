using Microsoft.EntityFrameworkCore;
using TheatricalPlayersRefactoringKata.API.Repositories.Interfaces;

namespace TheatricalPlayersRefactoringKata.API.Data;

public static class MigrationExtensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<ApiDbContext>(options =>
            options.UseNpgsql(config.GetConnectionString("DefaultConnection")));
        return services;
    }
}