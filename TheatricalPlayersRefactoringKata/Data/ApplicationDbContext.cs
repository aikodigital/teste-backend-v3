using Microsoft.EntityFrameworkCore;

namespace TheatricalPlayersRefactoringKata.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Play> Plays { get; set; }
        public DbSet<Performance> Performances { get; set; }
        public DbSet<Invoice> Invoices { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Invoice>()
                        .HasMany(i => i.Performances)
                        .WithOne(p => p.Invoice)
                        .HasForeignKey(p => p.InvoiceId);

            modelBuilder.Entity<Play>()
                        .HasMany(p => p.Performances)
                        .WithOne(pf => pf.Play)
                        .HasForeignKey(pf => pf.PlayId);
        }
    }
}
