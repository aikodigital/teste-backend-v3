using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Domain.Entities;

namespace TheatricalPlayersRefactoringKata.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Statement> Statements { get; set; }
        public DbSet<Domain.Entities.Performance> Performances { get; set; }
        public DbSet<Domain.Entities.Play> Plays { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Statement>()
        .HasMany(s => s.Performances)
        .WithOne(p => p.Statement)
        .HasForeignKey(p => p.StatementId)
        .OnDelete(DeleteBehavior.Cascade);
        

            modelBuilder.Entity<Domain.Entities.Performance>()
                .HasOne(p => p.Play)
                .WithMany()
                .HasForeignKey(p => p.PlayId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
