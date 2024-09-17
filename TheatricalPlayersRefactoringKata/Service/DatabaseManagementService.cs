using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TheatricalPlayersRefactoringKata.Context;

namespace TheatricalPlayersRefactoringKata.Service;
public static class DatabaseManagementService
{

    public static void MigrationInitialisation(IApplicationBuilder app)
    {
        using IServiceScope serviceScope = app.ApplicationServices.CreateScope();
        serviceScope.ServiceProvider.GetService<TheaterDbContext>().Database.Migrate();
    }
}
