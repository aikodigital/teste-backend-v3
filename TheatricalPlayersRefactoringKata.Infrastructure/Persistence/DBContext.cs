using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TheatricalPlayersRefactoringKata.Entities;

namespace TheatricalPlayersRefactoringKata.Infrastructure.Persistence
{
    public class DBContext : DbContext
    {
        private IConfiguration _config;
        public DbSet<Play> Plays { get; set; }

        public DBContext(IConfiguration config, DbContextOptions<DBContext> options) : base(options)
        {
            _config = config;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = _config.GetConnectionString("Postgresql");
            optionsBuilder.UseNpgsql(connectionString);
        }

    }
}
