using System;
using TheatricalPlayersRefactoringKata.Application.Models;
using TheatricalPlayersRefactoringKata.Application.Strategies;
using Xunit;

namespace TheatricalPlayersRefactoringKata.Tests.Strategies
{
    public class HistoryCalculationStrategyTests
    {
        [Fact]
        public void CalculateAmount_ShouldReturnSumOfTragedyAndComedyAmounts()
        {
            // Configura um cenário onde a peça é do tipo "history" e a audiência é de 25 pessoas.
            // Verifica se o valor calculado é a soma dos valores de tragédia e comédia.

            // Arrange
            var strategyFactory = new CalculationStrategyFactory();
            var strategy = new HistoryCalculationStrategy(strategyFactory);
            var play = new Play("History Play", 3000, "history");
            var performance = new Performance("1", 25);

            // Mock das estratégias de tragédia e comédia
            var tragedyStrategy = new TragedyCalculationStrategy();
            var comedyStrategy = new ComedyCalculationStrategy();

            // Act
            var amount = strategy.CalculateAmount(performance, play);

            // Assert
            Assert.Equal(tragedyStrategy.CalculateAmount(performance, play) + comedyStrategy.CalculateAmount(performance, play), amount);
        }

        [Fact]
        public void CalculateCredits_ShouldReturnCorrectCredits()
        {
            // Configura um cenário onde a peça é do tipo "history" e a audiência é de 35 pessoas.
            // Verifica se os créditos calculados estão corretos.

            // Arrange
            var strategyFactory = new CalculationStrategyFactory();
            var strategy = new HistoryCalculationStrategy(strategyFactory);
            var play = new Play("History Play", 3000, "history");
            var performance = new Performance("1", 35);

            // Act
            var credits = strategy.CalculateCredits(performance, play);

            // Assert
            Assert.Equal(5, credits);
        }
    }
}
