using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TheatricalPlayersRefactoringKata.Domain.Calculators;
using TheatricalPlayersRefactoringKata.Domain.Models;
using TheatricalPlayersRefactoringKata.Domain.Services;
using Xunit;

namespace TheatricalPlayersRefactoringKata.Tests
{
    public class StatementQueueServiceTests
    {
        [Fact]
        public async Task TestAsyncStatementXMLProcessing()
        {
            var plays = new Dictionary<string, Play>();
            plays.Add("hamlet", new Play("Hamlet", 4024, "tragedy"));
            plays.Add("as-like", new Play("As You Like It", 2670, "comedy"));
            plays.Add("othello", new Play("Othello", 3560, "tragedy"));
            plays.Add("henry-v", new Play("Henry V", 3227, "history"));
            plays.Add("john", new Play("King John", 2648, "history"));
            plays.Add("richard-iii", new Play("Richard III", 3718, "history"));


            var invoices = new List<Invoice>
            {
                new Invoice("BigCo",new List<Performance>
                {
                    new Performance("hamlet", 55),
                    new Performance("as-like", 35),
                    new Performance("othello", 40),
                    new Performance("henry-v", 20),
                    new Performance("john", 39),
                    new Performance("henry-v", 20)
                }),
                new Invoice("BigCo",new List<Performance>
                {
                    new Performance("hamlet", 55),
                    new Performance("as-like", 35),
                    new Performance("othello", 40),
                    new Performance("henry-v", 20),
                    new Performance("john", 39),
                    new Performance("henry-v", 20)
                })
            };

            var calculatorFactory = new TypeCalculatorFactory();
            var statementPrinter = new StatementPrinter(calculatorFactory);
            var formatter = new StatementPrinterXML();
            var outputDirectory = Path.Combine(Path.GetTempPath(), "StatementsTest");

            var logger = new LoggerFactory().CreateLogger<StatementQueueService>();
            var queueService = new StatementQueueService(statementPrinter, formatter, outputDirectory, logger);

            using var cts = new CancellationTokenSource();

            var processingTask = queueService.StartProcessingAsync(plays, cts.Token);

            foreach (var invoice in invoices)
            {
                await queueService.EnqueueInvoiceAsync(invoice);
            }

            while (queueService.PendingCount > 0)
            {
                await Task.Delay(100);
            }

            cts.Cancel();
            await processingTask;

            Assert.True(Directory.Exists(outputDirectory), "O diretório de saída não foi criado.");

            var generatedFiles = Directory.GetFiles(outputDirectory, "*.xml");
            Assert.Equal(invoices.Count, generatedFiles.Length);

            foreach (var file in generatedFiles)
            {
                var content = await File.ReadAllTextAsync(file);
                Assert.Contains("<Statement>", content);
            }

            Directory.Delete(outputDirectory, true);
        }
    }
}
