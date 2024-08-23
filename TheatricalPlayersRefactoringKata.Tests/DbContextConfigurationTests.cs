using Microsoft.EntityFrameworkCore;
using TheatricalPlayersRefactoringKata.Data;
using Xunit;

namespace TheatricalPlayersRefactoringKata.Tests
{
    public class DbContextConfigurationTests
    {
        private DbContextOptions<ApplicationDbContext> CreateInMemoryDbContextOptions()
        {
            return new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
        }

        [Fact]
        public void CanConfigureDbContext()
        {
            var options = CreateInMemoryDbContextOptions();

            using (var context = new ApplicationDbContext(options))
            {
                Assert.NotNull(context);
            }
        }
    }
}
