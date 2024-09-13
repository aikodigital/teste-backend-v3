using TheatricalPlayersRefactoringKata.Application.Calculator;
using TheatricalPlayersRefactoringKata.Entities;
using Xunit;

namespace TheatricalPlayersRefactoringKata.Tests
{
    public class HistoryAmountCalculatorTests
    {
        private HistoryAmountCalculator _calculator;
        public HistoryAmountCalculatorTests()
        {
            _calculator = new HistoryAmountCalculator();
        }

        [Fact]
        public void CalculateAmountShoulEqualTo5000()
        {
            // Arrange
            var performance = new Performance("1", 10);
            var baseAmount = 2000;

            // Act
            var amount = _calculator.CalculateAmount(performance, baseAmount);

            // Assert
            amount.Equals(7000);
        }

        [Fact]
        public void CalculateAmountShoulEqualTo26000()
        {
            // Arrange
            var performance = new Performance("1", 50);
            var baseAmount = 1000;

            // Act
            var amount = _calculator.CalculateAmount(performance, baseAmount);

            // Assert
            amount.Equals(62000);
        }

        [Fact] 
        public void CalculateEarnedCreditsShouldBe0()
        {
            // Arrange
            var audience = 10;

            // Act
            var earnedCredits = _calculator.CalculateEarnedCredits(audience);

            // Assert
            earnedCredits.Equals(0);
        }

        [Fact]
        public void CalculateEarnedCreditsShouldBe30()
        {
            // Arrange
            var audience = 60;

            // Act
            var earnedCredits = _calculator.CalculateEarnedCredits(audience);

            // Assert
            earnedCredits.Equals(30);
        }
    }
}
