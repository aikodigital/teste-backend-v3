#region

using System.Collections.Generic;
using ApprovalTests;
using ApprovalTests.Reporters;
using TheatricalPlayersRefactoringKata.Core;
using TheatricalPlayersRefactoringKata.Core.Entities;
using Xunit;

#endregion

namespace TheatricalPlayersRefactoringKata.Tests;

public class StatementPrinterTests
{
    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void TestStatementExampleLegacy()
    {
        var plays = new Dictionary<string, Play> {
            { "hamlet", new Play("Hamlet", 4024, "tragedy") },
            { "as-like", new Play("As You Like It", 2670, "comedy") },
            { "othello", new Play("Othello", 3560, "tragedy") }
        };

        var invoice = new Invoice(
            "BigCo",
            [
                new Performance(plays["hamlet"], 55),
                new Performance(plays["as-like"], 35),
                new Performance(plays["othello"], 40)
            ]
        );

        var result = StatementPrinter.Print(invoice);

        Approvals.Verify(result);
    }

    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void TestTextStatementExample()
    {
        var plays = new Dictionary<string, Play> {
            { "hamlet", new Play("Hamlet", 4024, "tragedy") },
            { "as-like", new Play("As You Like It", 2670, "comedy") },
            { "othello", new Play("Othello", 3560, "tragedy") },
            { "henry-v", new Play("Henry V", 3227, "history") },
            { "john", new Play("King John", 2648, "history") },
            { "richard-iii", new Play("Richard III", 3718, "history") }
        };

        var invoice = new Invoice(
            "BigCo",
            [
                new Performance(plays["hamlet"], 55),
                new Performance(plays["as-like"], 35),
                new Performance(plays["othello"], 40),
                new Performance(plays["henry-v"], 20),
                new Performance(plays["john"], 39),
                new Performance(plays["henry-v"], 20)
            ]
        );

        var result = StatementPrinter.Print(invoice);

        Approvals.Verify(result);
    }

    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void TestXmlStatementExample()
    {
        var plays = new Dictionary<string, Play> {
            { "hamlet", new Play("Hamlet", 4024, "tragedy") },
            { "as-like", new Play("As You Like It", 2670, "comedy") },
            { "othello", new Play("Othello", 3560, "tragedy") },
            { "henry-v", new Play("Henry V", 3227, "history") },
            { "john", new Play("King John", 2648, "history") },
            { "richard-iii", new Play("Richard III", 3718, "history") }
        };

        var invoice = new Invoice(
            "BigCo",
            [
                new Performance(plays["hamlet"], 55),
                new Performance(plays["as-like"], 35),
                new Performance(plays["othello"], 40),
                new Performance(plays["henry-v"], 20),
                new Performance(plays["john"], 39),
                new Performance(plays["henry-v"], 20)
            ]
        );

        var result = StatementPrinter.XmlPrint(invoice);

        Approvals.Verify(result);
    }
}