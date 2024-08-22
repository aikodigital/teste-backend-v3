
using Microsoft.EntityFrameworkCore;
using TheatricalPlayersRefactoringKata.Data;

public static class DbContextConfiguration
{
    public static ApplicationDbContext CreateInMemoryDbContext()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            
            .Options;

        return new ApplicationDbContext(options);
    }
}
