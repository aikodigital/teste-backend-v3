using Microsoft.EntityFrameworkCore;
using TheatricalPlayersAPI.Models;

namespace TheatricalPlayersAPI.Context;

public class TheatricalDbContext : DbContext
{
    private IConfiguration _configuration;
    public TheatricalDbContext(DbContextOptions<TheatricalDbContext> options) : base(options){
        options = options ?? throw new ArgumentNullException(nameof(options));
    }
    public DbSet<PerformanceModel> Performances { get; set; }
    public DbSet<InvoiceModel> Invoices { get; set; }
    public DbSet<PlayModel> Plays { get; set; }

    /*
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder){
        var typeDatabase = _configuration["TypeDatabase"];
        var connectionString = _configuration.GetConnectionString(typeDatabase);

        optionsBuilder.UseMySQL(connectionString);

    }*/
}