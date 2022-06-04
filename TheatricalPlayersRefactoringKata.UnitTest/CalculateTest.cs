using TheatricalPlayersRefactoringKata.Domain.Interface.Services;
using TheatricalPlayersRefactoringKata.Domain.Model.Enum;
using TheatricalPlayersRefactoringKata.Domain.Service;
using Xunit;

namespace TheatricalPlayersRefactoringKata.UnitTest
{
    public class CalculateTest
    {
        private readonly ICalculateService _calculateService;

        public CalculateTest()
        {
            _calculateService = new CalculateService();
        }

        [Theory]
        [InlineData(-1, 100)]
        [InlineData(0, 100)]
        [InlineData(1, 100)]
        [InlineData(10, 100)]
        [InlineData(100, 100)]
        [InlineData(1000, 100)]
        [InlineData(10000, 400)]
        public void CalculateBaseValue(int lines, decimal expectedResult)
        {
            decimal calculate = _calculateService.CalculateBaseValue(lines);

            Assert.True(calculate == expectedResult);
        }

        [Theory]
        [InlineData(-1, -1, 0)]
        [InlineData(0, 0, 0)]
        [InlineData(1, 0, 0)]
        [InlineData(0, 1, 0)]
        [InlineData(10, 15, 100)]
        [InlineData(100, 168, 800)]
        [InlineData(100, 1680, 868)]
        [InlineData(100, 5680, 1100)]
        [InlineData(300, 5680, 3100)]
        public void CalculateTragedyValue(int audience, int lines, decimal expectedResult)
        {
            decimal calculate = _calculateService.CalculateTragedyValue(audience, lines);

            Assert.True(calculate == expectedResult);
        }

        [Theory]
        [InlineData(-1, 0, 100)]
        [InlineData(0, 0, 100)]
        [InlineData(1, 0, 103)]
        [InlineData(10, 0, 130)]
        [InlineData(100, 1500, 450)]
        [InlineData(1000, 5400, 3400)]
        [InlineData(10000, 150, 30100)]
        public void CalculateComedyValue(int audience, int lines, decimal expectedResult)
        {
            decimal calculate = _calculateService.CalculateComedyValue(audience, lines);

            Assert.True(calculate == expectedResult);
        }

        [Theory]
        [InlineData(-1, 0, 100)]
        [InlineData(0, 900, 100)]
        [InlineData(1, 900, 203)]
        [InlineData(10, 1506, 330)]
        [InlineData(100, 2546, 1508)]
        [InlineData(1000, 3500, 13400)]
        [InlineData(10000, 5500, 130500)]
        public void CalculateHistoryValue(int audience, int lines, decimal expectedResult)
        {
            decimal calculate = _calculateService.CalculateHistoryValue(audience, lines);

            Assert.True(calculate == expectedResult);
        }

        [Theory]
        [InlineData(PlayTypeEnum.Tragedy, -1, 0, 0)]
        [InlineData(PlayTypeEnum.Tragedy, 0, 0, 0)]
        [InlineData(PlayTypeEnum.Tragedy, 100, 0, 0)]
        [InlineData(PlayTypeEnum.Tragedy, 200, 0, 0)]
        [InlineData(PlayTypeEnum.Tragedy, 1000, 0, 0)]
        [InlineData(PlayTypeEnum.Comedy, -1, 0, 100)]
        [InlineData(PlayTypeEnum.Comedy, 0, 0,100)]
        [InlineData(PlayTypeEnum.Comedy, 150, 0, 550)]
        [InlineData(PlayTypeEnum.Comedy, 2300, 0, 7000)]
        [InlineData(PlayTypeEnum.History, -1, 0, 100)]
        [InlineData(PlayTypeEnum.History, 0, 0, 100)]
        [InlineData(PlayTypeEnum.History, 450, 0, 1450)]
        [InlineData(PlayTypeEnum.History, 1450, 0, 4450)]
        public void CalculateValueByType(PlayTypeEnum playType, int audience,int lines, decimal expectedResult)
        {
            decimal calculate = _calculateService.CalculateValueByType(playType, audience, lines);

            Assert.True(calculate == expectedResult);
        }

        [Theory]
        [InlineData(PlayTypeEnum.Tragedy, -1, 0)]
        [InlineData(PlayTypeEnum.Tragedy, 0, 0)]
        [InlineData(PlayTypeEnum.Tragedy, 100, 70)]
        [InlineData(PlayTypeEnum.Tragedy, 200, 170)]
        [InlineData(PlayTypeEnum.Tragedy, 1000, 970)]
        [InlineData(PlayTypeEnum.Comedy, -1, 0)]
        [InlineData(PlayTypeEnum.Comedy, 0, 0)]
        [InlineData(PlayTypeEnum.Comedy, 150, 150)]
        [InlineData(PlayTypeEnum.Comedy, 2300, 2730)]
        [InlineData(PlayTypeEnum.History, -1, 0)]
        [InlineData(PlayTypeEnum.History, 0, 0)]
        [InlineData(PlayTypeEnum.History, 450, 420)]
        [InlineData(PlayTypeEnum.History, 1450, 1420)]
        public void CalculateCreditsByType(PlayTypeEnum playType, int audience, decimal expectedResult)
        {
            decimal calculate = _calculateService.CalculateCreditsByType(playType, audience);

            Assert.True(calculate == expectedResult);
        }

        [Theory]
        [InlineData(-1, 0)]
        [InlineData(0, 0)]
        [InlineData(1, 0)]
        [InlineData(10, 0)]
        [InlineData(100, 70)]
        [InlineData(1000, 970)]
        [InlineData(10000, 9970)]
        public void CalculateBaseCredits(int audience, decimal expectedResult)
        {
            decimal calculate = _calculateService.CalculateBaseCredits(audience);

            Assert.True(calculate == expectedResult);
        }

        [Theory]
        [InlineData(-1, 0)]
        [InlineData(0, 0)]
        [InlineData(1, 0)]
        [InlineData(10, 2)]
        [InlineData(100, 20)]
        [InlineData(1000, 200)]
        [InlineData(10000, 2000)]
        public void CalculateBonusCreditsComedy(int audience, decimal expectedResult)
        {
            decimal calculate = _calculateService.CalculateBonusCreditsComedy(audience);

            Assert.True(calculate == expectedResult);
        }
    }
}