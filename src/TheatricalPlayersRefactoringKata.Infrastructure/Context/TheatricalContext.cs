using Microsoft.EntityFrameworkCore;
using System.Reflection;
using TheatricalPlayersRefactoringKata.Entities;

namespace TheatricalPlayersRefactoringKata.Infrastructure.Context
{
    public class TheatricalContext : DbContext
    {
        public TheatricalContext(DbContextOptions<TheatricalContext> options) : base(options)
        {
        }

        public DbSet<Performance> Performances { get; set; }
        public DbSet<Invoice> Invoices { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasDefaultSchema("theatrical");

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
