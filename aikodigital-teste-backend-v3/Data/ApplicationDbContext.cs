using aikodigital_teste_backend_v3.Models;
using Microsoft.EntityFrameworkCore;

namespace aikodigital_teste_backend_v3.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Play> Plays { get; set; }
        public DbSet<Performance> Performance { get; set; }
        public DbSet<PerformanceStatement> PerformanceStatement { get; set; }

        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=Henrique;Trusted_Connection=True;TrustServerCertificate=True");
        }
    }
}
