using System;
using System.Collections.Generic;
using ApprovalTests;
using ApprovalTests.Reporters;
using TheatricalPlayersRefactoringKata.Enums;
using TheatricalPlayersRefactoringKata.Models;
using TheatricalPlayersRefactoringKata.Services;
using Xunit;

namespace TheatricalPlayersRefactoringKata.Tests;

public class StatementPrinterTests
{
    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void TestStatementExampleLegacy()
    {
        var plays = new Dictionary<string, Play>();
        plays.Add("hamlet", new Play("Hamlet", 4024, PlayType.Tragedy));
        plays.Add("as-like", new Play("As You Like It", 2670, PlayType.Comedy));
        plays.Add("othello", new Play("Othello", 3560, PlayType.Tragedy));

        Invoice invoice = new Invoice(
            "BigCo",
            new List<Performance>
            {
                new Performance(55, new Play("Hamlet", 4024, PlayType.Tragedy)),
                new Performance(35, new Play("As You Like It", 2670, PlayType.Comedy)),
                new Performance(40, new Play("Othello", 3560, PlayType.Tragedy)),
            }
        );

        StatementPrinter statementPrinter = new StatementPrinter();
        var result = statementPrinter.Print(invoice);

        Approvals.Verify(result);
    }

    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void TestTextStatementExample()
    {
        var plays = new Dictionary<string, Play>();
        plays.Add("hamlet", new Play("Hamlet", 4024, PlayType.Tragedy));
        plays.Add("as-like", new Play("As You Like It", 2670, PlayType.Comedy));
        plays.Add("othello", new Play("Othello", 3560, PlayType.Tragedy));
        plays.Add("henry-v", new Play("Henry V", 3227, PlayType.History));
        plays.Add("john", new Play("King John", 2648, PlayType.History));
        plays.Add("richard-iii", new Play("Richard III", 3718, PlayType.History));

        Invoice invoice = new Invoice(
            "BigCo",
            new List<Performance>
            {
                new Performance(55, plays["hamlet"]),
                new Performance(35, plays["as-like"]),
                new Performance(40, plays["othello"]),
                new Performance(20, plays["henry-v"]),
                new Performance(39, plays["john"]),
                new Performance(20, plays["henry-v"])
            }
        );

        StatementPrinter statementPrinter = new StatementPrinter();
        var result = statementPrinter.Print(invoice);

        Approvals.Verify(result);
    }
}
