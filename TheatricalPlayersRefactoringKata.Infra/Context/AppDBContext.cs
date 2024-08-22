using TheatricalPlayersRefactoringKata.Domain.Entity;
using TheatricalPlayersRefactoringKata.Application.Genres;
using Microsoft.EntityFrameworkCore;

namespace TheatricalPlayersRefactoringKata.Infra.Context;

public class AppDbContext : DbContext
{
    public DbSet<Play> Plays { get; set; }
    public DbSet<Invoice> Invoices { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=TheatricalPlayersRefactoringKata.Infra.db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Play>()
            .HasNoKey();
        
        modelBuilder.Entity<Play>()
            .HasDiscriminator<string>("PlayType")
            .HasValue<Tragedy>("Tragedy")
            .HasValue<Comedy>("Comedy")
            .HasValue<History>("History");

        base.OnModelCreating(modelBuilder);
    }
}
