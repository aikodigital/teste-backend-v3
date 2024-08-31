using System;
using TheatricalPlayersRefactoringKata.Application.Models;
using TheatricalPlayersRefactoringKata.Application.Strategies;
using Xunit;

namespace TheatricalPlayersRefactoringKata.Tests.Strategies
{
    public class ComedyCalculationStrategyTests
    {
        [Fact]
        public void CalculateAmount_ShouldReturnCorrectAmount()
        {
            // Configura um cenário onde a peça tem 3000 linhas e a audiência é de 25 pessoas.
            // Verifica se o valor calculado está correto.

            // Arrange
            var strategy = new ComedyCalculationStrategy();
            var play = new Play("Comedy Play", 3000, "comedy");
            var performance = new Performance("1", 25);

            // Act
            var amount = strategy.CalculateAmount(performance, play);

            // Assert
            Assert.Equal(50000, amount);
        }

        [Fact]
        public void CalculateAmount_ShouldApplyMinimumLines()
        {
            // Configura um cenário onde a peça tem 500 linhas (menor que o mínimo de 1000).
            // Verifica se o valor calculado aplica corretamente o mínimo de 1000 linhas.

            // Arrange
            var strategy = new ComedyCalculationStrategy();
            var play = new Play("Comedy Play", 500, "comedy");
            var performance = new Performance("1", 25);

            // Act
            var amount = strategy.CalculateAmount(performance, play);

            // Assert
            Assert.Equal(30000, amount);
        }

        [Fact]
        public void CalculateAmount_ShouldApplyMaximumLines()
        {
            // Configura um cenário onde a peça tem 5000 linhas (maior que o máximo de 4000).
            // Verifica se o valor calculado aplica corretamente o máximo de 4000 linhas.

            // Arrange
            var strategy = new ComedyCalculationStrategy();
            var play = new Play("Comedy Play", 5000, "comedy");
            var performance = new Performance("1", 25);

            // Act
            var amount = strategy.CalculateAmount(performance, play);

            // Assert
            Assert.Equal(60000, amount);
        }

        [Fact]
        public void CalculateCredits_ShouldReturnCorrectCredits()
        {
            // Configura um cenário onde a peça tem 3000 linhas e a audiência é de 35 pessoas.
            // Verifica se os créditos calculados estão corretos.

            // Arrange
            var strategy = new ComedyCalculationStrategy();
            var play = new Play("Comedy Play", 3000, "comedy");
            var performance = new Performance("1", 35);

            // Act
            var credits = strategy.CalculateCredits(performance, play);

            // Assert
            Assert.Equal(12, credits);
        }
    }
}
