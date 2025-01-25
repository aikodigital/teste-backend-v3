using Microsoft.EntityFrameworkCore;
using TheatricalPlayersRefactoringKata.Domain.Entities;


namespace TheatricalPlayersRefactoringKata.Infra.Contexto
{
    public class AppDbContext : DbContext
    {
        public DbSet<Invoice> Invoice { get; set; }
        public DbSet<Performance> Performance { get; set; }
        public DbSet<Play> Play { get; set; }


        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public AppDbContext() { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
