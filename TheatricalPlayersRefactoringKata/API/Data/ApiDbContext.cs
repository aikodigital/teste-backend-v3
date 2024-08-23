#region

using System.Data.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using TheatricalPlayersRefactoringKata.Core.Entities;
using DbContext = Microsoft.EntityFrameworkCore.DbContext;

#endregion

namespace TheatricalPlayersRefactoringKata.API.Data;

public sealed class ApiDbContext : DbContext
{
    public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
    {
        try
        {
            if (Database.GetService<IDatabaseCreator>() is not RelationalDatabaseCreator databaseCreator) return;
            if (!databaseCreator.CanConnect()) databaseCreator.Create();
            if (!databaseCreator.HasTables()) databaseCreator.CreateTables();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    public Microsoft.EntityFrameworkCore.DbSet<Invoice> Invoices { get; set; }
    public Microsoft.EntityFrameworkCore.DbSet<Performance> Performances { get; set; }
    public Microsoft.EntityFrameworkCore.DbSet<Play> Plays { get; set; }

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
        modelBuilder.Entity<Play>().Property(x => x.Name).HasMaxLength(30).IsRequired();
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
        modelBuilder.Entity<Performance>().HasOne(per => per.Play)
            .WithMany()
            .HasForeignKey(per => per.PlayId);

        return modelBuilder;
    }

    public static ModelBuilder InvoiceTable(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Invoice>().ToTable("Invoices");
        modelBuilder.Entity<Invoice>().HasKey(x => x.Id);
        modelBuilder.Entity<Invoice>().Property(x => x.Customer).HasMaxLength(30).IsRequired();

        return modelBuilder;
    }
}