using TheatherPlayerRefactoringKata.Invoice;
using Microsoft.EntityFrameworkCore;

namespace TheatherPlayersInfra.DataAccess;

internal class TheatherPlayersDbContext : DbContext
{
    public TheatherPlayersDbContext(DbContextOptions options) : base(options) { }
    public DbSet<Invoice> Invoices { get; set; }

}
