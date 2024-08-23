using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using TheatricalPlayersRefactoringKata.Core.Entities;

namespace TheatricalPlayersRefactoringKata.API.Data;

public class ApiDbContext(DbContextOptions<ApiDbContext> options) : DbContext(options)
{
    public DbSet<Invoice> Invoices { get; set; }
    public DbSet<Performance> Performances { get; set; }
    public DbSet<Play> Plays { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.PlayTable();
        modelBuilder.PerformanceTable();
        modelBuilder.InvoiceTable();
        
        base.OnModelCreating(modelBuilder);
    }
}

public static class ModelBuilderExtensions
{
    public static ModelBuilder PlayTable(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Play>().ToTable("Plays");
        modelBuilder.Entity<Play>().HasKey(x => x.Id);
        modelBuilder.Entity<Play>().Property(x => x.Name).IsRequired();
        modelBuilder.Entity<Play>().Property(x => x.Lines).IsRequired();
        modelBuilder.Entity<Play>().Property(x => x.Type).IsRequired();
        return modelBuilder;
    }

    public static ModelBuilder PerformanceTable(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Performance>().ToTable("Performances");
        modelBuilder.Entity<Performance>().HasKey(x => x.Id);
        modelBuilder.Entity<Performance>().Property(x => x.PlayId).IsRequired();
        modelBuilder.Entity<Performance>().Property(x => x.Audience).IsRequired();
        modelBuilder.Entity<Performance>().Property(x => x.Amount).IsRequired();
        modelBuilder.Entity<Performance>().HasOne(x => x.Play).WithMany().HasForeignKey(x => x.PlayId);
        modelBuilder.Entity<Performance>().HasMany(x => x.Invoices).WithMany(i => i.Performances)
            .UsingEntity(x => x.ToTable("PerformanceInvoices"));
        
        return modelBuilder;
    }
    
    public static ModelBuilder InvoiceTable(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Invoice>().ToTable("Invoices");
        modelBuilder.Entity<Invoice>().HasKey(x => x.Id);
        modelBuilder.Entity<Invoice>().Property(x => x.Customer).HasMaxLength(100).IsRequired();
        modelBuilder.Entity<Invoice>().HasMany(i => i.Performances).WithMany(p => p.Invoices)
            .UsingEntity(x => x.ToTable("PerformanceInvoices"));
        
        return modelBuilder;
    }
    
}