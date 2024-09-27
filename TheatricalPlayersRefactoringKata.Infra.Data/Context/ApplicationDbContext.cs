using Microsoft.EntityFrameworkCore;
using TheatricalPlayersRefactoringKata.Domain.Entities;
using TheatricalPlayersRefactoringKata.Domain.Interfaces.Repositories;

namespace TheatricalPlayersRefactoringKata.Infra.Data.Context;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }

    public DbSet<Invoice> Invoice { get; set; }
    public DbSet<Play> Play { get; set; }
    public DbSet<Performance> Performance { get; set; }
    public DbSet<TypeGenre> TypeGenre { get; set; }
    public DbSet<InvoiceProcess> InvoiceProcess { get; set; }
}