using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TheatricalPlayersRefactoringKata.Models;

namespace TheatricalPlayersRefactoringKata.infra
{
    internal class ApiDbContext : DbContext
    {
        private IConfiguration _configuration;
        public DbSet<Play>? Plays { get; set; }
        public DbSet<Performance>? Performances { get; set; }
        public DbSet<Invoice>? Invoices { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Play>()
                .HasKey(p => p.Id);

            modelBuilder.Entity<Performance>()
                .HasKey(p => p.Id);

            modelBuilder.Entity<Invoice>()
                .HasKey(i => i.Id);

            modelBuilder.Entity<Performance>()
                .HasOne(p => p.Play)
                .WithMany() 
                .HasForeignKey(p => p.PlayId);

            modelBuilder.Entity<Performance>()
                .HasOne(p => p.Invoice)
                .WithMany(i => i.Performances)
                .HasForeignKey(p => p.InvoiceId);

            base.OnModelCreating(modelBuilder);
        }



        public ApiDbContext(IConfiguration configuration, DbContextOptions options) : base(options)
        {
                _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var typeDataBase = _configuration["TypeDatabase"];
            var connectionString = _configuration.GetConnectionString(typeDataBase);

            optionsBuilder.UseNpgsql(connectionString);
        }
    }
}
