using TheatricalPlayersRefactoringKata.Models;
using TheatricalPlayersRefactoringKata.Calculators;
using Xunit;

namespace TheatricalPlayersRefactoringKata.Tests
{
    public class HistoryCalculatorTests
    {
        [Fact]
        public void CalculateAmount_ShouldReturnCorrectAmount()
        {
            // Arrange
            var calculator = new HistoryCalculator();
            var play = new Play("History Play", 2000, "history", calculator);
            var performance = new Performance("historyPlay", 30);

            // Act
            var amount = calculator.CalculateAmount(performance, play);

            // Assert
            double expectedAmount = 64000;
            Assert.Equal(expectedAmount, amount);
        }

        [Fact]
        public void CalculateCredits_ShouldReturnCorrectCredits()
        {
            // Arrange
            var calculator = new HistoryCalculator();
            var performance = new Performance("historyPlay", 30);

            // Act
            var credits = calculator.CalculateCredits(performance);

            // Assert
            int expectedCredits = 0;
            Assert.Equal(expectedCredits, credits);
        }
    }
}