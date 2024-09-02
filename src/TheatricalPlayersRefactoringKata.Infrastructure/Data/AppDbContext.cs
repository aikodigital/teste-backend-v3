using Microsoft.EntityFrameworkCore;
using TheatricalPlayersRefactoringKata.Domain.Models;

namespace TheatricalPlayersRefactoringKata.Infrastructure.Data;

/// <summary>
/// Database context for the Theatrical Players application.
/// </summary>
public class AppDbContext : DbContext
{
    public DbSet<Invoice> Invoices { get; set; } = null!;
    public DbSet<Customer> Customers { get; set; } = null!;
    public DbSet<Performance> Performances { get; set; } = null!;
    public DbSet<Play> Plays { get; set; } = null!;

    /// <summary>
    /// Initializes a new instance of the <see cref="AppDbContext"/> class.
    /// </summary>
    /// <param name="dbContextOptions">The options to be used by the DbContext.</param>
    public AppDbContext(DbContextOptions<AppDbContext> dbContextOptions)
        : base(dbContextOptions) {}

    /// <summary>
    /// Configures the model that was discovered by convention from the entity types
    /// exposed in <see cref="DbSet{TEntity}"/> properties on your derived context.
    /// </summary>
    /// <param name="modelBuilder">The builder being used to construct the model for this context.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configure string properties
        ConfigureStringProperties(modelBuilder);

        //// Apply configurations from the assembly where configuration classes are located
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

        // Pass the modelBuilder to the base class
        base.OnModelCreating(modelBuilder);
    }

    /// <summary>
    /// Sets all string properties to be of type varchar(100).
    /// </summary>
    /// <param name="modelBuilder">The builder being used to construct the model for this context.</param>
    private static void ConfigureStringProperties(ModelBuilder modelBuilder)
    {
        // Set all string properties to be of type varchar(100)
        foreach (var property in modelBuilder.Model
                     .GetEntityTypes()
                     .SelectMany(e => e.GetProperties()
                         .Where(p => p.ClrType == typeof(string))))

            property.SetColumnType("varchar(100)");
    }

    /// <summary>
    /// Commits all changes made in this context to the database.
    /// </summary>
    /// <returns>A task that represents the asynchronous save operation. The task result contains true if the changes were successfully committed; otherwise, false.</returns>
    public async Task<bool> Commit()
    {
        return await base.SaveChangesAsync() > 0;
    }
}