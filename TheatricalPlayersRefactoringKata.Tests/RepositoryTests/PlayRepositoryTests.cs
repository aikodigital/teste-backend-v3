using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Moq;
using TheatricalPlayersRefactoringKata.Enums;
using TheatricalPlayersRefactoringKata.Infra.DataBase;
using TheatricalPlayersRefactoringKata.Infra.DataBase.Repository;
using TheatricalPlayersRefactoringKata.Models;
using Xunit;

namespace TheatricalPlayersRefactoringKata.Tests.RepositoryTests
{
    public class PlayRepositoryTests
    {
        private readonly PlayRepository _repository;
        private readonly Mock<IDbContextFactory<TheatricalContext>> _mockFactory;
        private readonly DbContextOptions<TheatricalContext> _options;

        public PlayRepositoryTests()
        {
            _options = new DbContextOptionsBuilder<TheatricalContext>()
                .UseInMemoryDatabase(databaseName: "TheatricalDatabase")
                .Options;

            _mockFactory = new Mock<IDbContextFactory<TheatricalContext>>();
            _mockFactory.Setup(f => f.CreateDbContext()).Returns(new TheatricalContext(_options));

            _repository = new PlayRepository(_mockFactory.Object);
        }

        [Fact]
        public void CreatePlayTest()
        {

            var play = new Play("Odyssey", 1400, PlayType.History);
            _repository.CreatePlay(play);

            var createdPlay = _repository.GetPlayById(play.Id);
            Assert.NotNull(createdPlay);
            Assert.Equal(play.Name, createdPlay.Name);
        }

        [Fact]
        public void UpdatePlayTest()
        {
            var initialPlay = new Play("Odyssey", 1400, PlayType.History);
            _repository.CreatePlay(initialPlay);

            var createdPlay = _repository.GetPlayById(initialPlay.Id);
            Assert.NotNull(createdPlay);

            createdPlay.Name = "The Odyssey";
            createdPlay.Lines = 1500;
            createdPlay.Type = PlayType.Tragedy;

            _repository.UpdatePlay(createdPlay);

            var updatedPlay = _repository.GetPlayById(createdPlay.Id);
            Assert.NotNull(updatedPlay);
            Assert.Equal("The Odyssey", updatedPlay.Name);
            Assert.Equal(1500, updatedPlay.Lines);
            Assert.Equal(PlayType.Tragedy, updatedPlay.Type);
        }

        [Fact]
        public void DeletePlayTest()
        {
            var play = new Play("Odyssey", 1400, PlayType.History);
            _repository.CreatePlay(play);

            var createdPlay = _repository.GetPlayById(play.Id);
            Assert.NotNull(createdPlay);

            _repository.DeletePlay(play);

            var deletedPlay = _repository.GetPlayById(play.Id);
            Assert.Null(deletedPlay);
        }

        [Fact]
        public void GetAllPlaysTest()
        {
            var play1 = new Play("Odyssey", 1400, PlayType.History);
            var play2 = new Play("Hamlet", 1600, PlayType.Tragedy);
            var play3 = new Play("A Midsummer Night's Dream", 1200, PlayType.Comedy);

            _repository.CreatePlay(play1);
            _repository.CreatePlay(play2);
            _repository.CreatePlay(play3);

            var allPlays = _repository.GetAllPlays();

            Assert.NotNull(allPlays);
            Assert.Contains(allPlays, p => p.Name == play1.Name);
            Assert.Contains(allPlays, p => p.Name == play2.Name);
            Assert.Contains(allPlays, p => p.Name == play3.Name);
            Assert.Equal(3, allPlays.Count);
        }

    }
}
