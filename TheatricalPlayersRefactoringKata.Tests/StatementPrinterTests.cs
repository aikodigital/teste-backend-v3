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

            var result = _statementPrinter.Print(invoice, _plays);

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

            var result = _statementPrinter.Print(invoice, _plays);

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

            var result = _statementPrinter.Print(invoice, _plays, format: "xml");

            Approvals.Verify(result);
        }

        [Theory]
        [InlineData("tragedy", 55, 650.00, 25)]
        [InlineData("comedy", 35, 547.00, 7)]
        [InlineData("history", 20, 705.40, 0)]
        [InlineData("history", 39, 931.60, 9)]
        public void CalculateAmountAndCredits_ShouldReturnExpectedValues(string playType, int audience, decimal expectedAmount, int expectedCredits)
        {
            var play = new Play("Play", 3000, playType);
            var performance = new Performance("playId", audience);
            var calculator = new StatementCalculator();

            var (amount, credits) = calculator.CalculateAmountAndCredits(play, performance);

            Assert.Equal(expectedAmount, amount);
            Assert.Equal(expectedCredits, credits);
        }

        [Fact]
        public void FormatTextStatement_ShouldReturnExpectedTextFormat()
        {
            var invoice = new Invoice(
                "BigCo",
                new List<Performance>
                {
                    new Performance("hamlet", 55),
                    new Performance("as-like", 35)
                }
            );

            var formatter = new TextStatementFormatter();
            var result = formatter.FormatStatement(invoice, _plays, 1197.00m, 32);

            var expected = "Statement for BigCo\n  Hamlet: $650.00 (55 seats)\n  As You Like It: $547.00 (35 seats)\nAmount owed is $1,197.00\nYou earned 32 credits\n";
            Assert.Equal(expected, result);
        }

        [Fact]
        public void FormatXmlStatement_ShouldReturnExpectedXmlFormat()
        {
            var invoice = new Invoice(
                "BigCo",
                new List<Performance>
                {
                    new Performance("hamlet", 55),
                    new Performance("as-like", 35)
                }
            );

            var formatter = new XmlStatementFormatter();
            var result = formatter.FormatStatement(invoice, _plays, 1197.00m, 32);

            var expectedXml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>\n<Statement xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">\n  <Customer>BigCo</Customer>\n  <Items>\n    <Item>\n      <AmountOwed>650.00</AmountOwed>\n      <EarnedCredits>25</EarnedCredits>\n      <Seats>55</Seats>\n    </Item>\n    <Item>\n      <AmountOwed>547.00</AmountOwed>\n      <EarnedCredits>7</EarnedCredits>\n      <Seats>35</Seats>\n    </Item>\n  </Items>\n  <AmountOwed>1197.00</AmountOwed>\n  <EarnedCredits>32</EarnedCredits>\n</Statement>";

            Assert.Equal(expectedXml, result);
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

            Assert.Throws<KeyNotFoundException>(() => _statementPrinter.Print(invoice, _plays));
        }
    }
}
