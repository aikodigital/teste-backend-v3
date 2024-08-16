using Microsoft.EntityFrameworkCore;
using TheatricalPlayersAPI.Models;

namespace TheatricalPlayersAPI.Context;

public class TheatricalDbContext : DbContext
{
    public TheatricalDbContext(DbContextOptions<TheatricalDbContext> options) : base(options){
        
    }
    
    public DbSet<PerformanceModel> Performances { get; set; }
    public DbSet<InvoiceModel> Invoices { get; set; }
    public DbSet<PlayModel> Plays { get; set; }
}