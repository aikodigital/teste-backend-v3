using Microsoft.EntityFrameworkCore;
using TheatricalPlayersRefactoringKata.Domain.Entities;

namespace TheatricalPlayersRefactoringKata.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public DbSet<Invoice> Invoices { get; set; }
    public DbSet<Performance> Performances { get; set; }
    public DbSet<Play> Plays { get; set; }
    public DbSet<StatementResult> StatementResults { get; set; }
    public DbSet<StatementLine> StatementLines{ get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }
}
