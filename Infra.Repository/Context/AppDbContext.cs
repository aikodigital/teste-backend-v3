using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infra.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Play> Plays { get; set; }
    public DbSet<Invoice> Invoices { get; set; }
}
