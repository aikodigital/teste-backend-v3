using Microsoft.EntityFrameworkCore;
using TheatricalPlayersRefactoringKata.Model;

namespace TheatricalPlayersRefactoringKata.Context;
public class TheaterDbContext : DbContext
{
    public DbSet<Play> Plays { get; set; }
    public DbSet<Invoice> Invoices { get; set; }
    public DbSet<Performance> Performances { get; set; }

    public TheaterDbContext(DbContextOptions<TheaterDbContext> options)
        : base(options) {}
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Play>()
            .HasKey(p => p.PlayId);

        modelBuilder.Entity<Performance>()
            .HasKey(p => new { p.PerformanceId });

        modelBuilder.Entity<Invoice>()
            .HasKey(p => new { p.InvoiceId });
    }
}
