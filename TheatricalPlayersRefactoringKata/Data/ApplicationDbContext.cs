using Microsoft.EntityFrameworkCore;
using TheatricalPlayersRefactoringKata.Entities;

namespace TheatricalPlayersRefactoringKata.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Play> Plays { get; set; }
        public DbSet<Performance> Performances { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Performance>()
                        .HasOne(x => x.Invoice)
                        .WithMany(x => x.Performances)
                        .HasForeignKey(x => x.InvoiceId)
                        .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
