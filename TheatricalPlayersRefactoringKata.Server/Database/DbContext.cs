using Microsoft.EntityFrameworkCore;

using TheatricalPlayersRefactoringKata.Server.Database.Repositories.Entities.Play;

namespace TheatricalPlayersRefactoringKata.Server.Database
{
    public class DbContextTheatricalPlayers : DbContext
    {
        public DbContextTheatricalPlayers(DbContextOptions<DbContextTheatricalPlayers> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PlayEntityTypeConfiguration).Assembly);
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<PlayEntity> Plays { get; set; }
    }
}