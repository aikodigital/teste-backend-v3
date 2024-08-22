using System.Collections.Generic;
using ApprovalTests;
using ApprovalTests.Reporters;
using TheatricalPlayersRefactoringKata.Models;
using TheatricalPlayersRefactoringKata.Services;
using TheatricalPlayersRefactoringKata.Categories;
using Xunit;

namespace TheatricalPlayersRefactoringKata.Tests
{
    public class StatementPrinterTests
    {
        [Fact]
        [UseReporter(typeof(DiffReporter))]
        public void TestStatementExampleLegacy()
        {
            var playCategories = new Dictionary<string, IPlayCategory>
            {
                { "tragedy", new TragedyCategory() },
                { "comedy", new ComedyCategory() }
            };

            var plays = new Dictionary<int, Play>
            {
                { 1, new Play("Hamlet", "Tragedy") },
                { 2, new Play("As You Like It", "Comedy") },
                { 3, new Play("Othello", "Tragedy") }
            };

            var invoice = new Invoice(
                "BigCo",
                new List<Performance>
                {
                    new Performance(1, 55),
                    new Performance(2, 35),
                    new Performance(3, 40)
                }
            );

            var statementCalculator = new StatementCalculator(playCategories, plays);
            var statementPrinter = new StatementPrinter(statementCalculator);
            var result = statementPrinter.Print(invoice, plays); // Passando plays como Dictionary<int, Play>

            Approvals.Verify(result);
        }

        [Fact]
        [UseReporter(typeof(DiffReporter))]
        public void TestTextStatementExample()
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
                { 6, new Play("Richard III", "history") }
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
                    new Performance(4, 20)
                }
            );

            var statementCalculator = new StatementCalculator(playCategories, plays);
            var statementPrinter = new StatementPrinter(statementCalculator);
            var result = statementPrinter.Print(invoice, plays); // Passando plays como Dictionary<int, Play>

            Approvals.Verify(result);
        }
    }
}
