using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Application.Interfaces;
using TheatricalPlayersRefactoringKata.Application.Repository;
using TheatricalPlayersRefactoringKata.Application;
using TheatricalPlayersRefactoringKata.Domain.Exceptions;
using Xunit;
using TheatricalPlayersRefactoringKata.Application.Factory;
using TheatricalPlayersRefactoringKata.Application.Services;
using TheatricalPlayersRefactoringKata.Domain;
using TheatricalPlayersRefactoringKata.Application.Exceptions;

namespace TheatricalPlayersRefactoringKata.Tests
{
    public class ExceptionTests
    {
        private readonly StatementPrinter _StatementPrinter;

        public ExceptionTests()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddTransient<ICalculateBaseAmountPerLine,
                                           CalculateBaseAmountPerLine>();

            serviceCollection.AddTransient<ICalculateCreditAudience,
                                           CalculateCreditAudience>();

            serviceCollection.AddTransient<ICalculateAdditionalValuePerPlayType,
                                           CalculateAdditionalValuePerPlayType>();

            serviceCollection.AddTransient<IInvoicePrintFactory,
                                           InvoicePrintFactory>();

            serviceCollection.AddTransient<IInvoiceRepository,
                                           InvoiceRepository>();

            serviceCollection.AddTransient<StatementPrinter>();

            var serviceProvider = serviceCollection.BuildServiceProvider();
            _StatementPrinter = serviceProvider.GetService<StatementPrinter>()
                                ?? throw new InvalidOperationException("A instância não pôde ser criada.");
        }

        [Fact]
        public void ThrowsInvoiceNotFoundException()
        {
            var exception = Assert.Throws<InvoiceNotFoundException>(() => _StatementPrinter.PrintInvoice("999xxx", "Text"));
            Assert.Equal("Invoice with id 999xxx not found", exception.Message);
        }

        [Fact]
        public void ThrowsInvalidPrintTypeException()
        {
            var exception = Assert.Throws<InvalidPrintTypeException>(() => _StatementPrinter.PrintInvoice("1", "Json"));
            Assert.Equal("Unknown print type: Json", exception.Message);
        }
    }
}
