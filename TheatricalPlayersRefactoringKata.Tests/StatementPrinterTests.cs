using System.Collections.Generic;
using TheatricalPlayersRefactoringKata.Models;
using TheatricalPlayersRefactoringKata.Calculators;
using TheatricalPlayersRefactoringKata.Printing;
using ApprovalTests;
using ApprovalTests.Reporters;
using Xunit;

namespace TheatricalPlayersRefactoringKata.Tests;

public class StatementPrinterTests
{
   [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void TestStatementExampleLegacy()
    {
        var plays = new Dictionary<string, Play>
        {
            { "hamlet", new Play("Hamlet", 4024, "tragedy", new TragedyCalculator()) },
            { "as-like", new Play("As You Like It", 2670, "comedy", new ComedyCalculator()) },
            { "othello", new Play("Othello", 3560, "tragedy", new TragedyCalculator()) }
        };

        var invoice = new Invoice(
            "BigCo",
            new List<Performance>
            {
                new Performance("hamlet", 55),
                new Performance("as-like", 35),
                new Performance("othello", 40),
            }
        );

        var statementPrinter = new StatementPrinter();
        var result = statementPrinter.Print(invoice, plays);

        Approvals.Verify(result);
    }

    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void TestTextStatementExample()
    {
        var plays = new Dictionary<string, Play>
        {
            { "hamlet", new Play("Hamlet", 4024, "tragedy", new TragedyCalculator()) },
            { "as-like", new Play("As You Like It", 2670, "comedy", new ComedyCalculator()) },
            { "othello", new Play("Othello", 3560, "tragedy", new TragedyCalculator()) },
            { "henry-v", new Play("Henry V", 3227, "history", new HistoryCalculator()) },
            { "john", new Play("King John", 2648, "history", new HistoryCalculator()) },
            { "richard-iii", new Play("Richard III", 3718, "history", new HistoryCalculator()) }
        };

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

        var statementPrinter = new StatementPrinter();
        var result = statementPrinter.Print(invoice, plays);

        Approvals.Verify(result);

    }
}
