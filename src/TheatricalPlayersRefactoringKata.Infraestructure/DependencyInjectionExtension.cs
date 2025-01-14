using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TheatricalPlayersRefactoringKata.Domain.Repositories;
using TheatricalPlayersRefactoringKata.Domain.Repositories.Plays;
using TheatricalPlayersRefactoringKata.Infraestructure.DataAccess;
using TheatricalPlayersRefactoringKata.Infraestructure.DataAccess.Repositories;
using TheatricalPlayersRefactoringKata.Infraestructure.Services.FileGenerator;

namespace TheatricalPlayersRefactoringKata.Infraestructure
{
    public static class DependencyInjectionExtension
    {
        public static void AddInfraestructure(this IServiceCollection services, IConfiguration configuration)
        {
            AddServices(services);
            AddRepository(services);
            AddDbContext(services, configuration);
        }

        public static void AddServices(IServiceCollection services)
        {
            services.AddScoped<IFileGenerator, FileGenerator>();
        }

        public static void AddRepository(IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IPlaysWriteOnlyRepository, PlaysRepository>();
        }

        public static void AddDbContext(IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("Connection");
            var version = new Version(8, 0, 35);
            var serverVersion = new MySqlServerVersion(version);

            services.AddDbContext<TheatricalPlayersRefactoringKataDbContext>(config => config.UseMySql(connectionString, serverVersion));
        }
    }
}
