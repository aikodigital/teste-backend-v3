using Microsoft.EntityFrameworkCore;
using TheatricalPlayersRefactoringKata.Entities;

namespace TheatricalPlayersRefactoringKata;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
    public DbSet<Statement> Statements { get; set; }
    public DbSet<StatementItem> StatementItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Statement>()
            .HasMany(s => s.Items)
            .WithOne(i => i.Statement)
            .HasForeignKey(i => i.StatementId);

        base.OnModelCreating(modelBuilder);
    }
}
