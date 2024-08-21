using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Infra.DataBase.Repository;
using TheatricalPlayersRefactoringKata.Infra.DataBase;
using Xunit;
using TheatricalPlayersRefactoringKata.Enums;
using TheatricalPlayersRefactoringKata.Models;

namespace TheatricalPlayersRefactoringKata.Tests.RepositoryTests
{
    public class PerformanceRepositoryTests
    {
        private readonly PerformanceRepository _repository;
        private readonly Mock<IDbContextFactory<TheatricalContext>> _mockFactory;
        private readonly DbContextOptions<TheatricalContext> _options;

        public PerformanceRepositoryTests()
        {
            _options = new DbContextOptionsBuilder<TheatricalContext>()
                .UseInMemoryDatabase(databaseName: "TheatricalDatabase")
                .Options;

            _mockFactory = new Mock<IDbContextFactory<TheatricalContext>>();
            _mockFactory.Setup(f => f.CreateDbContext()).Returns(new TheatricalContext(_options));

            _repository = new PerformanceRepository(_mockFactory.Object);
        }

        [Fact]
        public void CreatePerformanceTest()
        {
            var play = new Play("Odyssey", 1400, PlayType.History);
            var performance = new Performance
            {
                Audience = 100,
                Play = play
            };
            _repository.CreatePerformance(performance);

            var createdPerformance = _repository.GetPerformanceById(performance.Id);
            Assert.NotNull(createdPerformance);
            Assert.Equal(performance.Audience, createdPerformance.Audience);
            Assert.Equal(play.Id, createdPerformance.PlayId);
        }

        [Fact]
        public void UpdatePerformanceTest()
        {
            var play = new Play("Odyssey", 1400, PlayType.History);
            var performance = new Performance
            {
                Audience = 100,
                Play = play
            };

            _repository.CreatePerformance(performance);

            var updatedPlay = new Play("Iliad", 1600, PlayType.Tragedy);
            var updatedPerformance = _repository.GetPerformanceById(performance.Id);
            Assert.NotNull(updatedPerformance);

            updatedPerformance.Audience = 100;
            updatedPerformance.Play = updatedPlay;

            _repository.UpdatePerformance(updatedPerformance);

            var fetchedPerformance = _repository.GetPerformanceById(updatedPerformance.Id);
            Assert.NotNull(fetchedPerformance);
            Assert.Equal(updatedPerformance.Audience, fetchedPerformance.Audience);
            Assert.Equal(updatedPlay.Id, fetchedPerformance.PlayId);
        }

        [Fact]
        public void DeletePerformanceTest()
        {
            var play = new Play("Odyssey", 1400, PlayType.History);
            var performance = new Performance
            {
                Audience = 100,
                Play = play
            };

            _repository.CreatePerformance(performance);

            var createdPerformance = _repository.GetPerformanceById(performance.Id);
            Assert.NotNull(createdPerformance);
            Assert.Equal(performance.Audience, createdPerformance.Audience);

            _repository.DeletePerformance(performance.Id);

            var deletedPerformance = _repository.GetPerformanceById(performance.Id);
            Assert.Null(deletedPerformance);
        }

        [Fact]
        public void GetAllPerformancesTest()
        {
            var play1 = new Play("Odyssey", 1400, PlayType.History);
            var play2 = new Play("Hamlet", 1600, PlayType.Tragedy);

            var performance1 = new Performance
            {
                Audience = 100,
                Play = play1
            };

            var performance2 = new Performance
            {
                Audience = 150,
                Play = play2
            };

            _repository.CreatePerformance(performance1);
            _repository.CreatePerformance(performance2);

            var performances = _repository.GetAllPerformances();

            Assert.NotNull(performances);
            Assert.Equal(2, performances.Count);
            Assert.Contains(performances, p => p.Id == performance1.Id && p.Audience == performance1.Audience);
            Assert.Contains(performances, p => p.Id == performance2.Id && p.Audience == performance2.Audience);
        }

    }
}
