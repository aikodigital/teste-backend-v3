using System;
using System.Collections.Generic;
using ApprovalTests;
using ApprovalTests.Reporters;
using Xunit;

namespace TheatricalPlayersRefactoringKata.Tests;

public class StatementPrinterTests
{
    [Obsolete]
    [UseReporter(typeof(RiderReporter))]
    public void TestStatementExampleLegacy()
    {
        var plays = new Dictionary<string, Play>();
        plays.Add("hamlet", new Play("Hamlet", 4024, Genres.TRAGEDY));
        plays.Add("as-like", new Play("As You Like It", 2670, Genres.COMEDY));
        plays.Add("othello", new Play("Othello", 3560, Genres.TRAGEDY));

        Invoice invoice = new Invoice(
            "BigCo",
            new List<Performance>
            {
                new Performance("hamlet", 55, Genres.TRAGEDY),
                new Performance("as-like", 35, Genres.COMEDY),
                new Performance("othello", 40, Genres.TRAGEDY),
            }
        );

        StatementPrinter statementPrinter = new StatementPrinter();
        var result = statementPrinter.Print(invoice, plays, StatementPrinter.TEXT_MODE);

        Approvals.Verify(result);
    }

    [Fact]
    [UseReporter(typeof(RiderReporter))]
    public void TestTextStatementExample()
    {
        var plays = new Dictionary<string, Play>();
        plays.Add("hamlet", new Play("Hamlet", 4024, Genres.TRAGEDY));
        plays.Add("as-like", new Play("As You Like It", 2670, Genres.COMEDY));
        plays.Add("othello", new Play("Othello", 3560, Genres.TRAGEDY));
        plays.Add("henry-v", new Play("Henry V", 3227, Genres.HISTORY));
        plays.Add("john", new Play("King John", 2648, Genres.HISTORY));
        plays.Add("richard-iii", new Play("Richard III", 3718, Genres.HISTORY));

        Invoice invoice = new Invoice(
            "BigCo",
            new List<Performance>
            {
                new Performance("hamlet", 55, Genres.TRAGEDY),
                new Performance("as-like", 35, Genres.COMEDY),
                new Performance("othello", 40, Genres.TRAGEDY),
                new Performance("henry-v", 20, Genres.HISTORY),
                new Performance("john", 39, Genres.HISTORY),
                new Performance("henry-v", 20, Genres.HISTORY),
            }
        );

        StatementPrinter statementPrinter = new StatementPrinter();
        var result = statementPrinter.Print(invoice, plays, StatementPrinter.TEXT_MODE);
        Approvals.Verify(result);
    }

    [Fact]
    [UseReporter(typeof(RiderReporter))]
    public void TestXmlStatementExample(){
        var plays = new Dictionary<string, Play>();
        plays.Add("hamlet", new Play("Hamlet", 4024, Genres.TRAGEDY));
        plays.Add("as-like", new Play("As You Like It", 2670, Genres.COMEDY));
        plays.Add("othello", new Play("Othello", 3560, Genres.TRAGEDY));
        plays.Add("henry-v", new Play("Henry V", 3227, Genres.HISTORY));
        plays.Add("john", new Play("King John", 2648, Genres.HISTORY));
        plays.Add("richard-iii", new Play("Richard III", 3718, Genres.HISTORY));

        Invoice invoice = new Invoice(
            "BigCo",
            new List<Performance>
            {
                new Performance("hamlet", 55, Genres.TRAGEDY),
                new Performance("as-like", 35, Genres.COMEDY),
                new Performance("othello", 40, Genres.TRAGEDY),
                new Performance("henry-v", 20, Genres.HISTORY),
                new Performance("john", 39, Genres.HISTORY),
                new Performance("henry-v", 20, Genres.HISTORY),
            }
        );
        StatementPrinter statementPrinter = new StatementPrinter();
        var result = statementPrinter.Print(invoice, plays, StatementPrinter.XML_MODE);
        Console.WriteLine(result);
        Approvals.Verify(result);
    }
}
