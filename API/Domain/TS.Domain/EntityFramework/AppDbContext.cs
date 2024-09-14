using Microsoft.EntityFrameworkCore;

namespace TS.Domain.EntityFramework
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {

    }
}