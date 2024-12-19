using Microsoft.EntityFrameworkCore;

namespace TheatricalPlayersRefactoringKata.API.Data
{
    public class TheaterAppDbContext : DbContext
    {
        public TheaterAppDbContext(DbContextOptions<TheaterAppDbContext> options) 
            : base(options)
        {
        }

        public DbSet<InvoiceRecordEntity> Invoices { get; set; }
    }
}
