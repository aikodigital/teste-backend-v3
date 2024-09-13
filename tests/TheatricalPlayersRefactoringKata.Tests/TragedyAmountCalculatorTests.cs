using System;
using TheatricalPlayersRefactoringKata.Application.Calculator;
using TheatricalPlayersRefactoringKata.Entities;
using Xunit;

namespace TheatricalPlayersRefactoringKata.Tests
{
    public class TragedyAmountCalculatorTests
    {
        private TragedyAmountCalculator _calculator;
        public TragedyAmountCalculatorTests()
        {
            _calculator = new TragedyAmountCalculator();
        }

        [Fact]
        public void CalculateAmountShoulEqualTo2000()
        {
            // Arrange
            var performance = new Performance("1", 10);
            var baseAmount = 2000;

            // Act
            var amount = _calculator.CalculateAmount(performance, baseAmount);

            // Assert
            amount.Equals(2000);
        }

        [Fact]
        public void CalculateAmountShoulEqualTo12000()
        {
            // Arrange
            var performance = new Performance("1", 40);
            var baseAmount = 2000;

            // Act
            var amount = _calculator.CalculateAmount(performance, baseAmount);

            // Assert
            amount.Equals(12000);
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
