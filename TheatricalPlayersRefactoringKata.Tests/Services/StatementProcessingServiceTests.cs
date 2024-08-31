using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using TheatricalPlayersRefactoringKata.Application.Interfaces;
using TheatricalPlayersRefactoringKata.Application.Models;
using TheatricalPlayersRefactoringKata.Application.Services;
using TheatricalPlayersRefactoringKata.Entities;
using Xunit;

namespace TheatricalPlayersRefactoringKata.Tests.Services
{
    public class StatementProcessingServiceTests : IDisposable
    {
        private readonly string _tempDirectory;

        public StatementProcessingServiceTests()
        {
            _tempDirectory = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            Directory.CreateDirectory(_tempDirectory);
        }

        public void Dispose()
        {
            if (Directory.Exists(_tempDirectory))
            {
                Directory.Delete(_tempDirectory, true);
            }
        }

        [Fact]
        public async Task ProcessInvoiceAsync_ShouldProcessInvoiceCorrectly()
        {
            // Configura um cenário onde uma fatura é processada e salva como um arquivo XML.
            // Verifica se a fatura é processada corretamente e se o arquivo XML é gerado.

            // Arrange
            var statementPrinterServiceMock = new Mock<IStatementPrinterService>();
            var configurationMock = new Mock<IConfiguration>();
            var serviceProviderMock = new Mock<IServiceProvider>();
            var statementServiceMock = new Mock<IStatementService>();

            configurationMock.Setup(c => c["BackgroundWorker:XmlDirectory"]).Returns(_tempDirectory);

            var serviceCollection = new ServiceCollection();
            serviceCollection.AddScoped(_ => statementServiceMock.Object);
            var serviceProvider = serviceCollection.BuildServiceProvider();

            serviceProviderMock.Setup(sp => sp.GetService(typeof(IServiceScopeFactory)))
                       .Returns(serviceProvider.GetService<IServiceScopeFactory>());

            var service = new StatementProcessingService(
                statementPrinterServiceMock.Object,
                configurationMock.Object,
                serviceProviderMock.Object);

            var invoice = new Invoice("John Doe", new List<Performance>
                {
                    new Performance("hamlet", 55),
                    new Performance("as-like", 35),
                    new Performance("henry-v", 25)
                });

            var statement = new Statement
            {
                Customer = "John Doe",
                Items = new List<StatementItem>(),
                TotalAmountOwed = 1975.4m,
                TotalEarnedCredits = 37
            };

            statementPrinterServiceMock.Setup(s => s.BuildStatement(invoice, It.IsAny<Dictionary<string, Play>>())).Returns(statement);
            statementPrinterServiceMock.Setup(s => s.Print(statement)).Returns("Formatted Statement");

            // Act
            await service.ProcessInvoiceAsync(invoice, CancellationToken.None);

            // Assert
            statementPrinterServiceMock.Verify(s => s.BuildStatement(invoice, It.IsAny<Dictionary<string, Play>>()), Times.Once);
            statementPrinterServiceMock.Verify(s => s.Print(statement), Times.Once);
            statementServiceMock.Verify(s => s.AddStatementAsync(statement), Times.Once);

            var filePath = Directory.GetFiles(_tempDirectory, $"{invoice.Customer}_*.xml").FirstOrDefault();
            Assert.NotNull(filePath);
            Assert.Equal("Formatted Statement", await File.ReadAllTextAsync(filePath));
        }

        [Fact]
        public async Task ExecuteAsync_ShouldProcessInvoicesFromQueue()
        {
            // Configura um cenário onde várias faturas são enfileiradas e processadas.
            // Verifica se as faturas são processadas corretamente a partir da fila.

            // Arrange
            var statementPrinterServiceMock = new Mock<IStatementPrinterService>();
            var configurationMock = new Mock<IConfiguration>();
            var serviceProviderMock = new Mock<IServiceProvider>();
            var statementServiceMock = new Mock<IStatementService>();

            configurationMock.Setup(c => c["BackgroundWorker:XmlDirectory"]).Returns(_tempDirectory);

            var serviceCollection = new ServiceCollection();
            serviceCollection.AddScoped(_ => statementServiceMock.Object);
            var serviceProvider = serviceCollection.BuildServiceProvider();

            serviceProviderMock.Setup(sp => sp.GetService(typeof(IServiceScopeFactory)))
                       .Returns(serviceProvider.GetService<IServiceScopeFactory>());

            var service = new StatementProcessingService(
                statementPrinterServiceMock.Object,
                configurationMock.Object,
                serviceProviderMock.Object);

            var invoice1 = new Invoice("John Doe", new List<Performance>
                {
                    new Performance("hamlet", 55),
                    new Performance("as-like", 35),
                    new Performance("henry-v", 25)
                });

            var invoice2 = new Invoice("Jane Doe", new List<Performance>
                {
                    new Performance("hamlet", 45),
                    new Performance("as-like", 30),
                    new Performance("henry-v", 20)
                });

            service.EnqueueInvoice(invoice1);
            service.EnqueueInvoice(invoice2);

            var statement1 = new Statement
            {
                Customer = "John Doe",
                Items = new List<StatementItem>(),
                TotalAmountOwed = 1975.4m,
                TotalEarnedCredits = 37
            };

            var statement2 = new Statement
            {
                Customer = "Jane Doe",
                Items = new List<StatementItem>(),
                TotalAmountOwed = 1675.4m,
                TotalEarnedCredits = 30
            };

            statementPrinterServiceMock.Setup(s => s.BuildStatement(invoice1, It.IsAny<Dictionary<string, Play>>())).Returns(statement1);
            statementPrinterServiceMock.Setup(s => s.BuildStatement(invoice2, It.IsAny<Dictionary<string, Play>>())).Returns(statement2);
            statementPrinterServiceMock.Setup(s => s.Print(statement1)).Returns("Formatted Statement 1");
            statementPrinterServiceMock.Setup(s => s.Print(statement2)).Returns("Formatted Statement 2");

            // Act
            await Task.Delay(500); // Aguarda um pouco para que as faturas sejam processadas
            await service.ProcessPendingInvoicesAsync(CancellationToken.None);

            // Assert
            statementPrinterServiceMock.Verify(s => s.BuildStatement(invoice1, It.IsAny<Dictionary<string, Play>>()), Times.Once);
            statementPrinterServiceMock.Verify(s => s.BuildStatement(invoice2, It.IsAny<Dictionary<string, Play>>()), Times.Once);
            statementPrinterServiceMock.Verify(s => s.Print(statement1), Times.Once);
            statementPrinterServiceMock.Verify(s => s.Print(statement2), Times.Once);
            statementServiceMock.Verify(s => s.AddStatementAsync(statement1), Times.Once);
            statementServiceMock.Verify(s => s.AddStatementAsync(statement2), Times.Once);

            var filePath1 = Directory.GetFiles(_tempDirectory, $"{invoice1.Customer}_*.xml").FirstOrDefault();
            var filePath2 = Directory.GetFiles(_tempDirectory, $"{invoice2.Customer}_*.xml").FirstOrDefault();
            Assert.NotNull(filePath1);
            Assert.NotNull(filePath2);
            Assert.Equal("Formatted Statement 1", await File.ReadAllTextAsync(filePath1));
            Assert.Equal("Formatted Statement 2", await File.ReadAllTextAsync(filePath2));
        }
    }
}
