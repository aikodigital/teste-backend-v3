using System.Collections.Generic;
using ApprovalTests;
using ApprovalTests.Reporters;
using TheatricalPlayersRefactoringKata.Models;
using TheatricalPlayersRefactoringKata.Services;
using TheatricalPlayersRefactoringKata.Categories;
using Xunit;

namespace TheatricalPlayersRefactoringKata.Tests
{
    public class XmlStatementPrinterTests
    {
        [Fact]
        [UseReporter(typeof(DiffReporter))]
        public void TestStatementXmlWithAllCategories()
        {
            var playCategories = new Dictionary<string, IPlayCategory>
            {
                { "tragedy", new TragedyCategory() },
                { "comedy", new ComedyCategory() },
                { "history", new HistoricalCategory() }
            };

            var plays = new Dictionary<int, Play>
            {
                { 1, new Play("Hamlet", "tragedy") },
                { 2, new Play("As You Like It", "comedy") },
                { 3, new Play("Othello", "tragedy") },
                { 4, new Play("Henry V", "history") },
                { 5, new Play("King John", "history") },
                { 6, new Play("Henry V", "history") }
            };

            var invoice = new Invoice(
                "BigCo",
                new List<Performance>
                {
                    new Performance(1, 55),
                    new Performance(2, 35),
                    new Performance(3, 40),
                    new Performance(4, 20),
                    new Performance(5, 39),
                    new Performance(6, 20)
                }
            );

            var statementCalculator = new StatementCalculator(playCategories, plays);
            var statementXmlPrinter = new XmlStatementPrinter(statementCalculator);
            var result = statementXmlPrinter.Print(invoice, plays);

            Approvals.Verify(result);
        }
    }
}
