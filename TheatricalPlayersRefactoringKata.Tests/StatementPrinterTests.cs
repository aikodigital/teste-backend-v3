using ApprovalTests;
using ApprovalTests.Reporters;
using System.Collections.Generic;
using TP.Domain.Entities;
using Xunit;

namespace TheatricalPlayersRefactoringKata.Tests
{
    public class StatementPrinterTests
    {
        private readonly StatementPrinterServices _statementPrinter;
        private readonly Dictionary<string, Play> _plays;

        public StatementPrinterTests()
        {
            _statementPrinter = new StatementPrinterServices();
            _plays = new Dictionary<string, Play>
            {
                { "hamlet", new Play("Hamlet", 4024, "tragedy") },
                { "as-like", new Play("As You Like It", 2670, "comedy") },
                { "othello", new Play("Othello", 3560, "tragedy") },
                { "henry-v", new Play("Henry V", 3227, "history") },
                { "john", new Play("King John", 2648, "history") }
            };
        }

        [Fact]
        [UseReporter(typeof(DiffReporter))]
        public void TestStatementExampleLegacy()
        {
            var invoice = new Invoice(
                "BigCo",
                new List<Performance>
                {
                    new Performance("hamlet", 55),
                    new Performance("as-like", 35),
                    new Performance("othello", 40)
                }
            );

            var result = _statementPrinter.Print(invoice, _plays, "text");

            Approvals.Verify(result);
        }

        [Fact]
        [UseReporter(typeof(DiffReporter))]
        public void TestTextStatementExample()
        {
            var invoice = new Invoice(
                "BigCo",
                new List<Performance>
                {
                    new Performance("hamlet", 55),
                    new Performance("as-like", 35),
                    new Performance("othello", 40),
                    new Performance("henry-v", 20),
                    new Performance("john", 39),
                    new Performance("henry-v", 20)
                }
            );

            var result = _statementPrinter.Print(invoice, _plays, "text");

            Approvals.Verify(result);
        }

        [Fact]
        [UseReporter(typeof(DiffReporter))]
        public void TestXmlStatementExample()
        {
            var invoice = new Invoice(
                "BigCo",
                new List<Performance>
                {
                    new Performance("hamlet", 55),
                    new Performance("as-like", 35),
                    new Performance("othello", 40),
                    new Performance("henry-v", 20),
                    new Performance("john", 39),
                    new Performance("henry-v", 20)
                }
            );

            var result = _statementPrinter.Print(invoice, _plays, "xml");

            Approvals.Verify(result);
        }

        [Fact]
        public void Print_ShouldThrowExceptionForInvalidPlayId()
        {
            var invoice = new Invoice(
                "BigCo",
                new List<Performance>
                {
                    new Performance("unknown-play-id", 55)
                }
            );

            Assert.Throws<KeyNotFoundException>(() => _statementPrinter.Print(invoice, _plays, "text"));
        }
    }
}
