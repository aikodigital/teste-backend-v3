using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace TheatricalPlayersRefactoringKata.Tests
{
    public class AsyncStatementProcessorTests
    {
        private const string OutputDirectory = "TestOutputs";

        public AsyncStatementProcessorTests()
        {
            if (Directory.Exists(OutputDirectory))
            {
                Directory.Delete(OutputDirectory, true);
            }
            Directory.CreateDirectory(OutputDirectory);
        }

        [Fact]
        public async Task TestAsyncStatementProcessing()
        {
            var plays = new Dictionary<string, Play>
            {
                ["hamlet"] = new Play("Hamlet", 4024, "tragedy"),
                ["as-like"] = new Play("As You Like It", 2670, "comedy"),
                ["othello"] = new Play("Othello", 3560, "tragedy")
            };

            var invoice = new Invoice(
                "BigCo",
                new List<Performance>
                {
                    new Performance("hamlet", 55),
                    new Performance("as-like", 35),
                    new Performance("othello", 40)
                }
            );

            var statementPrinter = new StatementPrinter();
            var processor = new AsyncStatementProcessor(statementPrinter, OutputDirectory);

            processor.EnqueueInvoice(invoice, plays);
            await processor.ProcessQueueAsync();

            var files = Directory.GetFiles(OutputDirectory, "*.xml");
            Assert.Single(files);

            var xmlContent = await File.ReadAllTextAsync(files[0]);
            Assert.Contains("<statement>", xmlContent);
            Assert.Contains("<customer>BigCo</customer>", xmlContent);
        }
    }
}
