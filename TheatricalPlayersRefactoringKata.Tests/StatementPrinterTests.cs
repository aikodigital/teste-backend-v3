using System;
using System.Collections.Generic;
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
            { "hamlet", new Play("Hamlet", 4024, Play.TypePlay.Tragedy) },
            { "as-like", new Play("As You Like It", 2670, Play.TypePlay.Comedy) },
            { "othello", new Play("Othello", 3560, Play.TypePlay.Tragedy) }
        };

        var invoice = new Invoice("BigCo", new List<Performance>
        {
            new ("hamlet", 55),
            new ("as-like", 35),
            new ("othello", 40)
        });

        var statementPrinter = new StatementPrinter();
        var result = StatementPrinter.Print(invoice, plays);

        Approvals.Verify(result);
    }

    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void TestTextStatementExample()
    {
        var plays = new Dictionary<string, Play>
        {
            { "hamlet", new Play("Hamlet", 4024, Play.TypePlay.Tragedy) },
            { "as-like", new Play("As You Like It", 2670, Play.TypePlay.Comedy) },
            { "othello", new Play("Othello", 3560, Play.TypePlay.Tragedy) },
            { "henry-v", new Play("Henry V", 3227, Play.TypePlay.History) },
            { "john", new Play("King John", 2648, Play.TypePlay.History) },
            { "richard-iii", new Play("Richard III", 3718, Play.TypePlay.History) }
        };

        var invoice = new Invoice("BigCo", new List<Performance>
        {
            new ("hamlet", 55),
            new ("as-like", 35),
            new ("othello", 40),
            new ("henry-v", 20),
            new ("john", 39),
            new ("richard-iii", 20)
        });

        var statementPrinter = new StatementPrinter();
        var result = StatementPrinter.Print(invoice, plays);

        Approvals.Verify(result);
    }
}
