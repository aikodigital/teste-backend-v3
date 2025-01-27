using Microsoft.EntityFrameworkCore;
using TheatricalPlayersRefactoringKata.Domain.Entities;


namespace TheatricalPlayersRefactoringKata.Infra.Contexto
{
    public class AppDbContext : DbContext
    {
        public DbSet<InvoiceEntity> Invoice { get; set; }
        public DbSet<PerformanceEntity> Performance { get; set; }
        public DbSet<PlayEntity> Play { get; set; }


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
