using Microsoft.EntityFrameworkCore;
using TheatricalPlayersRefactoringKata.Infra.DataBase.Mapping;
using TheatricalPlayersRefactoringKata.Models;

namespace TheatricalPlayersRefactoringKata.Infra.DataBase
{
    internal class TheatricalContext : DbContext
    {
        public TheatricalContext(DbContextOptions<TheatricalContext> options) : base(options) { }

        public DbSet<Play> Plays { get; set; }
        public DbSet<Performance> Performances { get; set; }
        public DbSet<Invoice> Invoices { get; set; }

        protected override void OnModelCreating(ModelBuilder mb)
        {
            new PerformanceMap(mb.Entity<Performance>());
            base.OnModelCreating(mb);
        }
    }
}
