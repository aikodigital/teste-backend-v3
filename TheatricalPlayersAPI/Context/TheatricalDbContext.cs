using TheatricalPlayersRefactoringKata;
using Microsoft.EntityFrameworkCore;
using TheatricalPlayersAPI.Models;

namespace ApprovalTests.Approvers;

public class TheatricalDbContext : DbContext
{
    public TheatricalDbContext(DbContextOptions<TheatricalDbContext> options) : base(options){
    }
    
    public DbSet<PerformanceModel> Performances { get; set; }
    public DbSet<InvoiceModel> Invoices { get; set; }
}