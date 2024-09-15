using TheatricalPlayersRefactoringKata.Domain;
using TheatricalPlayersRefactoringKata.Domain.Enums;
using Xunit;

namespace TheatricalPlayersRefactoringKata.Tests.Strategies
{
    public class PlayTypeStrategiesTest
    {
        [Theory]
        [InlineData("As You Like It", 2670, 35, 547)]
        public void TestPlayTypeComedyAmountCalculatorByAudience(string playName, int lines, int audience, decimal expectedResult)
        {
            var comedyPlay = new Play(playName, lines, PlayTypeEnum.Comedy);

            var result = comedyPlay.CalculateAmountValueByAudience(audience);

            Assert.Equal(expectedResult, result);
        }

        [Theory]
        [InlineData("Hamlet", 4024, 55, 650)]
        [InlineData("Othello", 3560, 40, 456)]
        public void TestPlayTypeTragedyAmountCalculatorByAudience(string playName, int lines, int audience, decimal expectedResult)
        {
            var tradedyPlay = new Play(playName, lines, PlayTypeEnum.Tragedy);

            var result = tradedyPlay.CalculateAmountValueByAudience(audience);

            Assert.Equal(expectedResult, result);
        }

        [Theory]
        [InlineData("Henry V", 3227, 20, 705.40)]
        [InlineData("King John", 2648, 39, 931.60)]
        public void TestPlayTypeHistoryAmountCalculatorByAudience(string playName, int lines, int audience, decimal expectedResult)
        {
            var historyPlay = new Play(playName, lines, PlayTypeEnum.History);

            var result = historyPlay.CalculateAmountValueByAudience(audience);

            Assert.Equal(expectedResult, result);
        }

        [Theory]
        [InlineData("As You Like It", 2670, 35, 12)]
        public void TestePlayTypeComedyCreditCalculatorByAudience(string playName, int lines, int audience, decimal expectedResult)
        {
            var comedyPlay = new Play(playName, lines, PlayTypeEnum.Comedy);

            var result = comedyPlay.CalculateCreditsByAudience(audience);

            Assert.Equal(expectedResult, result);
        }

        [Theory]
        [InlineData("Hamlet", 4024, 55, 25)]
        [InlineData("Othello", 3560, 40, 10)]
        public void TestPlayTypeTragedyCreditCalculadorByAudience(string playName, int lines, int audience, decimal expectedResult)
        {
            var tragedyPlay = new Play(playName, lines, PlayTypeEnum.Tragedy);

            var result = tragedyPlay.CalculateCreditsByAudience(audience);

            Assert.Equal(expectedResult, result);
        }

        [Theory]
        [InlineData("Henry V", 3227, 20, 0)]
        [InlineData("King John", 2648, 39, 9)]
        public void TestPlayTypeHistoryCreditCalculadorByAudience(string playName, int lines, int audience, decimal expectedResult)
        {
            var historyPlay = new Play(playName, lines, PlayTypeEnum.History);

            var result = historyPlay.CalculateCreditsByAudience(audience);

            Assert.Equal(expectedResult, result);
        }
    }
}
