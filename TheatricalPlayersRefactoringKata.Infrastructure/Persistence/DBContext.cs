using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TheatricalPlayersRefactoringKata.Entities;

namespace TheatricalPlayersRefactoringKata.Infrastructure.Persistence
{
    public class DBContext : DbContext
    {
        private IConfiguration _config;
        public DbSet<Play> Plays { get; set; }
        public DbSet<Performance> Performances { get; set; }
        public DbSet<Invoice> Invoices { get; set; }

        public DBContext(IConfiguration config, DbContextOptions<DBContext> options) : base(options)
        {
            _config = config;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = _config.GetConnectionString("Postgresql");
            optionsBuilder.UseNpgsql(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Play>()
                .HasKey(p => p.Name);

            modelBuilder.Entity<Play>()
                .HasIndex(p => p.Name)
                .IsUnique();

            modelBuilder.Entity<Performance>()
                .HasKey(p => p.Id);

            modelBuilder.Entity<Performance>()
                .HasOne(p => p.Play)
                .WithMany()
                .HasForeignKey(p => p.PlayId) 
                .HasPrincipalKey(p => p.Name)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_Performance_PlayId");

            modelBuilder.Entity<Invoice>()
                .HasKey(i => i.Id);

            modelBuilder.Entity<Invoice>()
                .HasMany(i => i.Performances)
                .WithOne(p => p.Invoice)
                .HasForeignKey(p => p.InvoiceId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
