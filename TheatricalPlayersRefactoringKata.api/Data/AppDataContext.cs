using Microsoft.EntityFrameworkCore;
using TheatricalPlayersRefactoringKata.Models;


namespace TheatricalPlayersRefactoringKata.api.Data
{
    public class AppDataContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Play> Plays { get; set; }
        public DbSet<Performance> Performances { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
    }
}
