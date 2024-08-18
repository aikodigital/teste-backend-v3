using System;
using System.Collections.Generic;
using ApprovalTests;
using ApprovalTests.Reporters;
using TheatricalPlayersRefactoringKata.Enums;
using TheatricalPlayersRefactoringKata.Models;
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
                new Performance("hamlet", 55, new Play("Hamlet", 4024, PlayType.Tragedy)),
                new Performance("as-like", 35, new Play("As You Like It", 2670, PlayType.Comedy)),
                new Performance("othello", 40, new Play("Othello", 3560, PlayType.Tragedy)),
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
                new Performance("hamlet", 55, new Play("Hamlet", 4024, PlayType.Tragedy)),
                new Performance("as-like", 35, new Play("As You Like It", 2670, PlayType.Comedy)),
                new Performance("othello", 40, new Play("Othello", 3560, PlayType.Tragedy)),
                new Performance("henry-v", 20, new Play("Henry V", 3227, PlayType.History)),
                new Performance("john", 39, new Play("King John", 2648, PlayType.History)),
                new Performance("henry-v", 20, new Play("Henry V", 3227, PlayType.History))
            }
        );

        StatementPrinter statementPrinter = new StatementPrinter();
        var result = statementPrinter.Print(invoice, plays);

        Approvals.Verify(result);
    }
}
