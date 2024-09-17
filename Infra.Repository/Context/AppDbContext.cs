using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infra.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Statement> Statement { get; set; }
    public DbSet<Item> Item { get; set; }
}
