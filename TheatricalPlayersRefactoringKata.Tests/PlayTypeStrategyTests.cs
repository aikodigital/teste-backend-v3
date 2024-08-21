using System;
using TheatricalPlayersRefactoringKata.Factories;
using TheatricalPlayersRefactoringKata.Strategies;
using Xunit;

namespace TheatricalPlayersRefactoringKata.Tests
{
    public class PlayTypeStrategyTests
    {
        [Fact]
        public void ComedyStrategy_ShouldCalculateCorrectAmount()
        {
            var strategy = new ComedyStrategy();
            var result = strategy.Execute(1000, 30);

            Assert.Equal(25000, result);
        }

        [Fact]
        public void HistoryStrategy_ShouldCalculateCorrectAmount()
        {
            var strategy = new HistoryStrategy();
            var result = strategy.Execute(1000, 35);

            Assert.Equal(35000, result);
        }

        [Fact]
        public void TragedyStrategy_ShouldCalculateCorrectAmount()
        {
            var strategy = new TragedyStrategy();
            var result = strategy.Execute(1000, 35);

            Assert.Equal(6000, result);
        }

        [Fact]
        public void PlayStrategyFactory_ShouldReturnCorrectStrategy()
        {
            Assert.IsType<ComedyStrategy>(PlayStrategyFactory.GetStrategy("comedy"));
            Assert.IsType<HistoryStrategy>(PlayStrategyFactory.GetStrategy("history"));
            Assert.IsType<TragedyStrategy>(PlayStrategyFactory.GetStrategy("tragedy"));
        }

        [Fact]
        public void PlayStrategyFactory_ShouldThrowExceptionForInvalidType()
        {
            Assert.Throws<ArgumentException>(() => PlayStrategyFactory.GetStrategy("invalid"));
        }
    }
}
