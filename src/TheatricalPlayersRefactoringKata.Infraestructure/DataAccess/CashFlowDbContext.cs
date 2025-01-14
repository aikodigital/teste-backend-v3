using Microsoft.EntityFrameworkCore;
using TheatricalPlayersRefactoringKata.Domain.Entities;

namespace TheatricalPlayersRefactoringKata.Infraestructure.DataAccess
{
    public class CashFlowDbContext : DbContext
    {
        public CashFlowDbContext(DbContextOptions options) : base(options) { }
        public DbSet<Play> Play { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Play>().HasKey(p => p.Id);
        }
    }
}
