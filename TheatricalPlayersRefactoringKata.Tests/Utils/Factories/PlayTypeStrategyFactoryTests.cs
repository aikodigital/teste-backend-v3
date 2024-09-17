using System;
using TheatricalPlayersRefactoringKata.Enums;
using TheatricalPlayersRefactoringKata.Factories;
using TheatricalPlayersRefactoringKata.Strategies;
using Xunit;

namespace TheatricalPlayersRefactoringKata.Tests.Factories
{
    public class PlayTypeStrategyFactoryTests
    {
        [Fact]
        public void GetStrategy_ShouldReturnTragedyStrategy_ForTragedyPlayType()
        {
            PlayType playType = PlayType.Tragedy;

            var strategy = PlayTypeStrategyFactory.GetStrategy(playType);

            Assert.IsType<TragedyStrategy>(strategy);
        }

        [Fact]
        public void GetStrategy_ShouldReturnComedyStrategy_ForComedyPlayType()
        {
            PlayType playType = PlayType.Comedy;

            var strategy = PlayTypeStrategyFactory.GetStrategy(playType);

            Assert.IsType<ComedyStrategy>(strategy);
        }

        [Fact]
        public void GetStrategy_ShouldReturnHistoricalStrategy_ForHistoryPlayType()
        {
            PlayType playType = PlayType.History;

            var strategy = PlayTypeStrategyFactory.GetStrategy(playType);

            Assert.IsType<HistoricalStrategy>(strategy);
        }

        [Fact]
        public void GetStrategy_ShouldThrowArgumentException_ForUnknownPlayType()
        {
            PlayType unknownPlayType = (PlayType)999;

            var exception = Assert.Throws<ArgumentException>(() => PlayTypeStrategyFactory.GetStrategy(unknownPlayType));
            Assert.Equal("Unknown play type: 999", exception.Message);
        }
    }
}

