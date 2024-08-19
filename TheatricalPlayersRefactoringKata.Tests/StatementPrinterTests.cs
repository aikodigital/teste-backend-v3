using System;
using System.Collections.Generic;
using ApprovalTests;
using ApprovalTests.Reporters;
using Xunit;
using TheatricalPlayersRefactoringKata.Strategies;

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

            var invoice = new Invoice("BigCo", new List<Performance>
            {
                new Performance("hamlet", 55),
                new Performance("as-like", 35),
                new Performance("othello", 40)
            });

            var statementPrinter = new StatementPrinter();
            var result = statementPrinter.Print(invoice, plays);

            Approvals.Verify(result);
        }

        [Fact]
        [UseReporter(typeof(DiffReporter))]
        public void TestStatementWithHistoryPlay()
        {
            var plays = new Dictionary<string, Play>
            {
                { "henry-v", new Play("Henry V", 3227, "history") },
                { "john", new Play("King John", 2648, "history") },
                { "richard-iii", new Play("Richard III", 3718, "history") }
            };

            var invoice = new Invoice("BigCo", new List<Performance>
            {
                new Performance("henry-v", 20),
                new Performance("john", 39)
            });

            var statementPrinter = new StatementPrinter();
            var result = statementPrinter.Print(invoice, plays);

            Approvals.Verify(result);
        }

        [Fact]
        public void TestTragedyAmountCalculation()
        {
            var strategy = new TragedyPlayStrategy();
            double amount = strategy.CalculateAmount(55, 4000);
            Assert.Equal(65000, amount); 
        }

        [Fact]
        public void TestComedyAmountCalculation()
        {
            var strategy = new ComedyPlayStrategy();
            double amount = strategy.CalculateAmount(35, 2670);
            Assert.Equal(54700, amount); 
        }

        [Fact]
        public void TestHistoryAmountCalculation()
        {
            var strategy = new HistoryPlayStrategy();
            double amount = strategy.CalculateAmount(39, 3000);
            Assert.Equal(100200, amount); 
        }

        [Fact]
        public void TestTragedyCreditsCalculation()
        {
            var strategy = new TragedyPlayStrategy();
            int credits = strategy.CalculateCredits(55);
            Assert.Equal(25, credits); 
        }

        [Fact]
        public void TestComedyCreditsCalculation()
        {
            var strategy = new ComedyPlayStrategy();
            int credits = strategy.CalculateCredits(35);
            Assert.Equal(12, credits);
        }

        [Fact]
        public void TestHistoryCreditsCalculation()
        {
            var strategy = new HistoryPlayStrategy();
            int credits = strategy.CalculateCredits(39);
            Assert.Equal(9, credits);
        }
    }
}
