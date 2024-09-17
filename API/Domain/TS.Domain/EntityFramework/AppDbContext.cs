using Microsoft.EntityFrameworkCore;
using TS.Domain.Entities;

namespace TS.Domain.EntityFramework
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Invoice> Invoices { get; set; }
        public virtual DbSet<Performance> Performances { get; set; }
        public virtual DbSet<Play> Plays { get; set; }
    }
}