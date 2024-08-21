using TheatricalPlayersRefactoringKata.Models;
using TheatricalPlayersRefactoringKata.Calculators;
using Xunit;

namespace TheatricalPlayersRefactoringKata.Tests
{
    public class ComedyCalculatorTests
    {
        [Fact]
        public void CalculateAmount_ShouldReturnCorrectAmount()
        {
            // Arrange
            var calculator = new ComedyCalculator();
            var play = new Play("Comedy Play", 2000, "comedy", calculator);
            var performance = new Performance("comedyPlay", 30);

            // Act
            var amount = calculator.CalculateAmount(performance, play);

            // Assert
            double expectedAmount = 44000;
            Assert.Equal(expectedAmount, amount);
        }

        [Fact]
        public void CalculateCredits_ShouldReturnCorrectCredits()
        {
            // Arrange
            var calculator = new ComedyCalculator();
            var performance = new Performance("comedyPlay", 30);

            // Act
            var credits = calculator.CalculateCredits(performance);

            // Assert
            int expectedCredits = 6;
            Assert.Equal(expectedCredits, credits);
        }
    }
}
