using Microsoft.EntityFrameworkCore;
using TheatricalPlayersRefactoringKata.Domain.Entities;
using TheatricalPlayersRefactoringKata.Infrastructure.Configurations;

namespace TheatricalPlayersRefactoringKata.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Play> Plays { get; set; }
        public DbSet<Performance> Performances { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new PlayConfiguration());
            modelBuilder.ApplyConfiguration(new PerformanceConfiguration());
            modelBuilder.ApplyConfiguration(new InvoiceConfiguration());
        }
    }
}
