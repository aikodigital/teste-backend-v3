using System.Collections.Generic;
using Moq;
using TheatricalPlayersRefactoringKata.Application.Interfaces;
using TheatricalPlayersRefactoringKata.Application.Models;
using TheatricalPlayersRefactoringKata.Application.Services;
using TheatricalPlayersRefactoringKata.Application.Strategies;
using TheatricalPlayersRefactoringKata.Entities;
using Xunit;

namespace TheatricalPlayersRefactoringKata.Tests.Services
{
    public class StatementPrinterServiceTests
    {
        [Fact]
        public void BuildStatement_ShouldReturnCorrectStatement()
        {
            // Configura um cenário onde há uma fatura com duas performances e dois tipos de peças diferentes.
            // Verifica se a declaração gerada está correta.

            // Arrange
            var strategyFactory = new CalculationStrategyFactory();

            var formatterAdapterMock = new Mock<IFormatterAdapter>();
            var service = new StatementPrinterService(strategyFactory, formatterAdapterMock.Object);

            var plays = new Dictionary<string, Play>
            {
                { "hamlet", new Play("Hamlet", 3000, "tragedy") },
                { "as-like", new Play("As You Like It", 3000, "comedy") },
                { "henry-v", new Play("Henry V", 3227, "history") }
            };

            var invoice = new Invoice("John Doe", new List<Performance>
            {
                new Performance("hamlet", 55),
                new Performance("as-like", 35),
                new Performance("henry-v", 25)
            });

            // Act
            var statement = service.BuildStatement(invoice, plays);

            // Assert
            Assert.Equal("John Doe", statement.Customer);
            Assert.Equal(3, statement.Items.Count);
            Assert.Equal((decimal)1975.4, statement.TotalAmountOwed);
            Assert.Equal(37, statement.TotalEarnedCredits);
        }

        [Fact]
        public void Print_ShouldReturnFormattedStatement()
        {
            // Configura um cenário onde há uma fatura com duas performances e dois tipos de peças diferentes.
            // Verifica se a declaração formatada está correta.

            // Arrange
            var strategyFactory = new CalculationStrategyFactory();

            var formatterAdapterMock = new Mock<IFormatterAdapter>();
            formatterAdapterMock.Setup(f => f.Format(It.IsAny<Statement>())).Returns("Formatted Statement");

            var service = new StatementPrinterService(strategyFactory, formatterAdapterMock.Object);

            var plays = new Dictionary<string, Play>
            {
                { "hamlet", new Play("Hamlet", 3000, "tragedy") },
                { "as-like", new Play("As You Like It", 3000, "comedy") },
                { "henry-v", new Play("Henry V", 3227, "history") }
            };

            var invoice = new Invoice("John Doe", new List<Performance>
            {
                new Performance("hamlet", 55),
                new Performance("as-like", 35),
                new Performance("henry-v", 25)
            });

            // Act
            var formattedStatement = service.Print(invoice, plays);

            // Assert
            Assert.Equal("Formatted Statement", formattedStatement);
        }
    }
}
