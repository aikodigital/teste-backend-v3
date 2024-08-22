using Microsoft.EntityFrameworkCore;
using TheatricalPlayersRefactoringKata.Data;

namespace TheatricalPlayersRefactoringKata.Tests
{
    public static class DbContextConfiguration
    {
        public static ApplicationDbContext CreateInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("TestDatabase")
                .Options;

            return new ApplicationDbContext(options);
        }
    }
}
