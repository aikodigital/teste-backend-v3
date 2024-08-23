using Microsoft.EntityFrameworkCore;
using TheatricalPlayersRefactoringKata.Models;

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
            modelBuilder.Entity<Performance>()
                .HasOne(p => p.Play)
                .WithMany()
                .HasForeignKey(p => p.PlayId);


        }
    }
}
