using Xunit;

namespace TheatricalPlayersRefactoringKata.Tests
{
    public class ComedyAmountCalculatorTests
    {
        private ComedyAmountCalculator _calculator;
        public ComedyAmountCalculatorTests()
        {
            _calculator = new ComedyAmountCalculator();
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
            amount.Equals(5000);
        }

        [Fact]
        public void CalculateAmountShoulEqualTo26000()
        {
            // Arrange
            var performance = new Performance("1", 30);
            var baseAmount = 2000;

            // Act
            var amount = _calculator.CalculateAmount(performance, baseAmount);

            // Assert
            amount.Equals(26000);
        }

        [Fact]
        public void CalculateEarnedCreditsShouldBe0()
        {
            // Arrange
            var audience = 10;

            // Act
            var earnedCredits = _calculator.CalculateEarnedCredits(audience);

            // Assert
            earnedCredits.Equals(2);
        }

        [Fact]
        public void CalculateEarnedCreditsShouldBe30()
        {
            // Arrange
            var audience = 60;

            // Act
            var earnedCredits = _calculator.CalculateEarnedCredits(audience);

            // Assert
            earnedCredits.Equals(42);
        }
    }
}
