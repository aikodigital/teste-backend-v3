using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata;
using TheatricalPlayersRefactoringKata.Models;
using Xunit;

namespace TheatricalPlayersRefactoringKata.Tests
{
    public class AsyncStatementProcessorTests
    {
        [Fact]
        public async Task GenerateXmlAsync_ShouldSaveFile()
        {
            var invoice = new Invoice("BigCo", new List<Performance>
            {
                new Performance("hamlet", 55),
                new Performance("as-like", 35),
                new Performance("othello", 40)
            });
            var plays = new Dictionary<string, Play>
            {
                { "hamlet", new Play("Hamlet", 4024, "tragedy") },
                { "as-like", new Play("As You Like It", 2670, "comedy") },
                { "othello", new Play("Othello", 3560, "tragedy") }
            };

            var processor = new AsyncStatementProcessor();
            var filePath = Path.Combine(Path.GetTempPath(), "test_statement.xml");
            await processor.GenerateXmlAsync(invoice, plays, filePath);

            Assert.True(File.Exists(filePath));

            // Optional: Validate the content of the file
            var content = await File.ReadAllTextAsync(filePath);
            Assert.Contains("<invoice>", content); // Example check for XML content

            File.Delete(filePath);
        }
    }
}
