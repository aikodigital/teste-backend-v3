using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Domain;
using TheatricalPlayersRefactoringKata.Domain.Entities;
using TheatricalPlayersRefactoringKata.Domain.Enum;

namespace TheatricalPlayersRefactoringKata.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TheatricalPlayersRefactoringKata.db");
            optionsBuilder.UseSqlite($"Data Source={dbPath}");
        }

        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<InvoiceCalculeteSettings> InvoiceCalculeteSettings { get; set; }
        public DbSet<InvoiceCreditSettings> InvoiceCreditSettings { get; set; }
        public DbSet<Performance> Performances { get; set; }
        public DbSet<Play> Play { get; set; }
        public DbSet<PlayType> PlayType { get; set; }

        //add-migration -Context ApplicationDbContext InitialMigration -v
        //update-database -Context ApplicationDbContext -v
    }
}
