using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace TheatricalPlayersRefactoringKata.infra
{
    internal class ApiDbContext : DbContext
    {
        private IConfiguration _configuration;

        public ApiDbContext(IConfiguration configuration, DbContextOptions options) : base(options)
        {
                _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var typeDataBase = _configuration["TypeDatabase"];
            var connectionString = _configuration.GetConnectionString(typeDataBase);

            optionsBuilder.UseNpgsql(connectionString);
        }
    }
}
