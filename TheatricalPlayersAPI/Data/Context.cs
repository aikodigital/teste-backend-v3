using Microsoft.EntityFrameworkCore;
using TheatricalPlayersRefactoringKata.Models;

namespace TheatricalPlayersRefactoringKata.API.Data
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options)
            : base(options)
        {
        }

        public DbSet<Extrato> Extrato { get; set; } = default!;
        public DbSet<Play> Play { get; set; } = default!;
    }
}
