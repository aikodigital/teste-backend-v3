using System.Collections.Generic;
using ApprovalTests;
using ApprovalTests.Reporters;
using Moq;
using Xunit;
using TheatricalPlayersRefactoringKata.Services;
using TheatricalPlayersRefactoringKata.Models;
using TheatricalPlayersRefactoringKata.Services.Formatters;
using TheatricalPlayersRefactoringKata.Services.PlayType;

namespace TheatricalPlayersRefactoringKata.Tests
{
    public class StatementPrinterTests
    {
        [Fact]
        [UseReporter(typeof(DiffReporter))]
        public void TestStatementExampleLegacy()
        {
            var plays = new Dictionary<string, Play>
            {
                { "hamlet", new Play("Hamlet", 4024, "tragedy") },
                { "as-like", new Play("As You Like It", 2670, "comedy") },
                { "othello", new Play("Othello", 3560, "tragedy") }
            };

            Invoice invoice = new Invoice(
                "BigCo",
                new List<Performance>
                {
                    new Performance("hamlet", 55),
                    new Performance("as-like", 35),
                    new Performance("othello", 40)
                }
            );

            StatementPrinter statementPrinter = new StatementPrinter();
            var result = statementPrinter.Print(invoice, plays);

            Approvals.Verify(result);
        }

        [Fact]
        [UseReporter(typeof(DiffReporter))]
        public void TestTextStatementExample()
        {
            var plays = new Dictionary<string, Play>
            {
                { "hamlet", new Play("Hamlet", 4024, "tragedy") },
                { "as-like", new Play("As You Like It", 2670, "comedy") },
                { "othello", new Play("Othello", 3560, "tragedy") },
                { "henry-v", new Play("Henry V", 3227, "history") },
                { "john", new Play("King John", 2648, "history") },
                { "richard-iii", new Play("Richard III", 3718, "history") }
            };

            Invoice invoice = new Invoice(
                "BigCo",
                new List<Performance>
                {
                    new Performance("hamlet", 55),
                    new Performance("as-like", 35),
                    new Performance("othello", 40),
                    new Performance("henry-v", 20),
                    new Performance("john", 39),
                    new Performance("henry-v", 20),
                }
            );

            StatementPrinter statementPrinter = new StatementPrinter();
            var result = statementPrinter.Print(invoice, plays);

            Approvals.Verify(result);
        }

        [Fact]
        [UseReporter(typeof(DiffReporter))]
        public void TestXmlStatementExample()
        {
            var plays = new Dictionary<string, Play>
            {
                { "hamlet", new Play("Hamlet", 4024, "tragedy") },
                { "as-like", new Play("As You Like It", 2670, "comedy") },
                { "othello", new Play("Othello", 3560, "tragedy") },
                { "henry-v", new Play("Henry V", 3227, "history") },
                { "john", new Play("King John", 2648, "history") },
                { "richard-iii", new Play("Richard III", 3718, "history") }
            };

            Invoice invoice = new Invoice(
                "BigCo",
                new List<Performance>
                {
                    new Performance("hamlet", 55),
                    new Performance("as-like", 35),
                    new Performance("othello", 40),
                    new Performance("henry-v", 20),
                    new Performance("john", 39),
                    new Performance("henry-v", 20),
                }
            );

            StatementPrinter statementPrinter = new StatementPrinter();
            var result = statementPrinter.PrintXml(invoice, plays);

            Approvals.Verify(result);
        }

        [Fact]
        public void TestGenerateStatement()
        {
            var plays = new Dictionary<string, Play>
            {
                { "hamlet", new Play("Hamlet", 4024, "tragedy") }
            };

            var invoice = new Invoice(
                "BigCo",
                new List<Performance>
                {
                    new Performance("hamlet", 30)
                }
            );

            var statementPrinter = new StatementPrinter();
            var statement = statementPrinter.GenerateStatement(invoice, plays);

            Assert.NotNull(statement);
            Assert.Single(statement.Items);
            Assert.Equal(30, statement.Items[0].Seats);
            Assert.Equal("Hamlet", statement.Items[0].PlayName);
        }

        [Fact]
        public void TestFormatterLogic()
        {
            var formatterMock = new Mock<IStatementFormatter>();
            formatterMock.Setup(x => x.Format(It.IsAny<Statement>())).Returns("Mock result");

            var statement = new Statement { Customer = "BigCo" };
            var result = formatterMock.Object.Format(statement);

            Assert.Equal("Mock result", result);
        }
    }
}
