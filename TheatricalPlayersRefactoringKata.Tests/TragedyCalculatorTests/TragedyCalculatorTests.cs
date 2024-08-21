using TheatricalPlayersRefactoringKata.Models;
using TheatricalPlayersRefactoringKata.Calculators;
using Xunit;

namespace TheatricalPlayersRefactoringKata.Tests
{
    public class TragedyCalculatorTests
    {
        [Fact]
        public void CalculateAmount_ShouldReturnCorrectAmount()
        {
            // Arrange
            var calculator = new TragedyCalculator();
            var play = new Play("Tragedy Play", 2000, "tragedy", calculator);
            var performance = new Performance("tragedyPlay", 40);

            // Act
            var amount = calculator.CalculateAmount(performance, play);

            // Assert
            double expectedAmount = 30000;
            Assert.Equal(expectedAmount, amount);
        }

        [Fact]
        public void CalculateCredits_ShouldReturnCorrectCredits()
        {
            // Arrange
            var calculator = new TragedyCalculator();
            var performance = new Performance("tragedyPlay", 40);

            // Act
            var credits = calculator.CalculateCredits(performance);

            // Assert
            int expectedCredits = 10;
            Assert.Equal(expectedCredits, credits);
        }
    }
}
