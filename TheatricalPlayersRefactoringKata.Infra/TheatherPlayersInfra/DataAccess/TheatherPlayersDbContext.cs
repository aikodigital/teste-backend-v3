using Microsoft.EntityFrameworkCore;
using TheatricalPlayersRefactoringKata.Domain.Entities;

namespace TheatherPlayersInfra.DataAccess;

internal class TheatherPlayersDbContext : DbContext
{
    public TheatherPlayersDbContext(DbContextOptions options) : base(options) { }
    public DbSet<Invoice> Invoices { get; set; }

}
