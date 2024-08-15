
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TheatherPlayersInfra.DataAccess;
using TheatherPlayersInfra.DataAccess.Repos;
using TheatricalPlayersRefactoringKata.Domain.Repos;

namespace TheatherPlayersInfra;

public static class DependencyInjection
{
    public static void AddInfra(this IServiceCollection services, IConfiguration config)
    {
        AddRepos(services, config);
        AddDbContext(services, config);
    }

    private static void AddRepos(IServiceCollection services, IConfiguration config)
    {
        services.AddScoped<IUnityOfWork, UnitOfWork>();
        services.AddScoped<IInvoice, InvoiceRepo>();
    }

    private static void AddDbContext(IServiceCollection services, IConfiguration config)
    {
        var connectionString = config.GetConnectionString("Connection");
        var serverVersion = new MySqlServerVersion(new Version(8, 0, 38));

        services.AddDbContext<TheatherPlayersDbContext>(config => config.UseMySql(connectionString, serverVersion));
    }
}
