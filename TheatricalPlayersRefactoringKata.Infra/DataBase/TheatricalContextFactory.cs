using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace TheatricalPlayersRefactoringKata.Infra.DataBase
{
    public class TheatricalContextFactory : IDesignTimeDbContextFactory<TheatricalContext>
    {
        public TheatricalContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<TheatricalContext>();
            var connectionString = "Server=localhost;Port=3307;User=root;Password=M@r1ll10n;Database=theatricalplayersdb";
            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));

            return new TheatricalContext(optionsBuilder.Options);
        }
    }
}
