using TheatricalPlayersRefactoringKata.Domain.Entity;
using TheatricalPlayersRefactoringKata.Application.Genres;
using Microsoft.EntityFrameworkCore;

namespace TheatricalPlayersRefactoringKata.Infra.Context;

public class AppDBContext : DbContext
{
    private DbSet<Play> Play { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=TheatricalPlayersRefactoringKata.Infra.db");
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Play>().HasNoKey()
            .HasDiscriminator<string>("PlayType")
            .HasValue<Tragedy>("Tragedy")
            .HasValue<Comedy>("Comedy")
            .HasValue<History>("History");

        base.OnModelCreating(modelBuilder);
    }

}