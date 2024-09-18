using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Application;
using TheatricalPlayersRefactoringKata.Application.Constants;
using TheatricalPlayersRefactoringKata.Application.Factory;
using TheatricalPlayersRefactoringKata.Application.Interfaces;
using TheatricalPlayersRefactoringKata.Application.Repository;
using TheatricalPlayersRefactoringKata.Application.Services;
using TheatricalPlayersRefactoringKata.Domain;
using TheatricalPlayersRefactoringKata.Domain.Entities;
using Xunit;

namespace TheatricalPlayersRefactoringKata.Tests
{
    public class StatementPrinterQueueTests
    {
        private readonly StatementPrinter _StatementPrinter;

        public StatementPrinterQueueTests()
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
                                     ?? throw new InvalidOperationException("A instância de CalculateBaseAmountPerLine não pôde ser criada.");
        }

        [Fact]
        public async Task EnqueueAndProcessStatements_ShouldProcessQueueSuccessfully()
        {
            var invoice = new Invoice(1, "BigCo");
            var performances = new List<Performance> { new Performance(1, 1, 50, 1) };
            var plays = new List<Play> { new Play(1, "Hamlet", 4024, 1) };
            var settings = GenerateInvoiceCalculeteSettings();
            var credits = GenerateInvoiceCreditSettings();
            var path = $@"{AppDomain.CurrentDomain.BaseDirectory}invoices";

            _StatementPrinter.EnqueueInvoiceForProcessing(invoice);

            var processTask = _StatementPrinter.ProcessInvoicesAsync(path);

            await Task.Delay(5000);

            var xmlPath = $@"{path}\{invoice.InvoiceId}.xml";
            Assert.True(File.Exists(xmlPath));

            if (File.Exists(xmlPath)) File.Delete(xmlPath);
            if (Directory.Exists(path)) Directory.Delete(path);
        }

        #region Generate
        private static List<InvoiceCalculeteSettings> GenerateInvoiceCalculeteSettings()
        {
            var invoiceCalculeteSettings = new List<InvoiceCalculeteSettings>();
            invoiceCalculeteSettings.Add(new InvoiceCalculeteSettings(
                        1,
                        1,
                        StatementPrinterConstants.TRAGEDY_MINIMUM_AUDIENCE,
                        StatementPrinterConstants.TRAGEDY_BONUS,
                        StatementPrinterConstants.TRAGEDY_PER_AUDIENCE_ADDITIONAL,
                        StatementPrinterConstants.TRAGEDY_PER_AUDIENCE
            ));
            invoiceCalculeteSettings.Add(new InvoiceCalculeteSettings(
                        2,
                        2,
                        StatementPrinterConstants.COMEDY_MINIMUM_AUDIENCE,
                        StatementPrinterConstants.COMEDY_BONUS,
                        StatementPrinterConstants.COMEDY_PER_AUDIENCE_ADDITIONAL,
                        StatementPrinterConstants.COMEDY_PER_AUDIENCE
            ));
            invoiceCalculeteSettings.Add(new InvoiceCalculeteSettings(
                        3,
                        3,
                        StatementPrinterConstants.TRAGEDY_MINIMUM_AUDIENCE,
                        StatementPrinterConstants.TRAGEDY_BONUS,
                        StatementPrinterConstants.TRAGEDY_PER_AUDIENCE_ADDITIONAL,
                        StatementPrinterConstants.TRAGEDY_PER_AUDIENCE
            ));
            invoiceCalculeteSettings.Add(new InvoiceCalculeteSettings(
                        4,
                        3,
                        StatementPrinterConstants.COMEDY_MINIMUM_AUDIENCE,
                        StatementPrinterConstants.COMEDY_BONUS,
                        StatementPrinterConstants.COMEDY_PER_AUDIENCE_ADDITIONAL,
                        StatementPrinterConstants.COMEDY_PER_AUDIENCE
            ));
            return invoiceCalculeteSettings;
        }
        private static List<InvoiceCreditSettings> GenerateInvoiceCreditSettings()
        {
            var InvoiceCreditSettings = new List<InvoiceCreditSettings>()
        {
            new InvoiceCreditSettings(1, 1, 30, 0),
            new InvoiceCreditSettings(2, 2, 30, 5m),
            new InvoiceCreditSettings(3, 3, 30, 0)
        };
            return InvoiceCreditSettings;
        }
        #endregion
    }
}
