using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using TheatricalPlayersRefactoringKata.Data;
using TheatricalPlayersRefactoringKata.Models;
using Xunit;

namespace TheatricalPlayersRefactoringKata.Tests
{
    public class IntegrationTests
    {
        private static IConfigurationRoot _configuration;

        static IntegrationTests()
        {
            _configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory()) 
                .AddJsonFile("appsettings.Test.json")         
                .Build();
        }

        private DbContextOptions<ApplicationDbContext> CreatePostgreSqlDbContextOptions()
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException("A string de conexão 'DefaultConnection' não foi encontrada no arquivo de configuração.");
            }

            return new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseNpgsql(connectionString)
                .Options;
        }

        [Fact]
        public void CanInsertAndRetrievePlay()
        {
            var options = CreatePostgreSqlDbContextOptions();

            using (var context = new ApplicationDbContext(options))
            {
                var play = new Play { Title = "Hamlet", Category = "Tragedy" };
                context.Plays.Add(play);
                context.SaveChanges();

                var retrievedPlay = context.Plays.Find(play.Id);

                Assert.NotNull(retrievedPlay);
                Assert.Equal(play.Title, retrievedPlay.Title);
            }
        }
    }
}
