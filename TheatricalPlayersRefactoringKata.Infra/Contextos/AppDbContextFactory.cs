using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using TheatricalPlayersRefactoringKata.Infra.Contexto;


namespace Sistema.microsservice.Infra.Configuracao.Contextos
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var conn = "Host=localhost;Port=5432;Database=TheatricalPlayers;Username=postgres;Password=1234";
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseNpgsql(conn);

            return new AppDbContext(optionsBuilder.Options);
        }
    }

}
