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
        var plays = new Dictionary<string, Play>();
        plays.Add("hamlet", new TragedyPlay("Hamlet", 4024));
        plays.Add("as-like", new ComedyPlay("As You Like It", 2670));
        plays.Add("othello", new TragedyPlay("Othello", 3560));

        Invoice invoice = new Invoice(
            "BigCo",
            new List<Performance>
            {
                new Performance(plays["hamlet"], 55),
                new Performance(plays["as-like"], 35),
                new Performance(plays["othello"], 40),
            }
        );

        var textPrinter = new TextStatementFormatter();
        StatementPrinter statementPrinter = new StatementPrinter(textPrinter);
        var result = statementPrinter.Print(invoice);

        Approvals.Verify(result);
    }

    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void TestTextStatementExample()
    {
        var plays = new Dictionary<string, Play>();
        plays.Add("hamlet", new TragedyPlay("Hamlet", 4024));
        plays.Add("as-like", new ComedyPlay("As You Like It", 2670));
        plays.Add("othello", new TragedyPlay("Othello", 3560));
        plays.Add("henry-v", new HistoricalPlay("Henry V", 3227));
        plays.Add("john", new HistoricalPlay("King John", 2648));
        plays.Add("richard-iii", new HistoricalPlay("Richard III", 3718));

        Invoice invoice = new Invoice(
            "BigCo",
            new List<Performance>
            {
                new Performance(plays["hamlet"], 55),
                new Performance(plays["as-like"], 35),
                new Performance(plays["othello"], 40),
                new Performance(plays["henry-v"], 20),
                new Performance(plays["john"], 39),
                new Performance(plays["henry-v"], 20)
            }
        );

        var textPrinter = new TextStatementFormatter();
        StatementPrinter statementPrinter = new StatementPrinter(textPrinter);
        var result = statementPrinter.Print(invoice);

        Approvals.Verify(result);
    }
    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void TestXmlStatementExample()
    {
        var plays = new Dictionary<string, Play>();
        plays.Add("hamlet", new TragedyPlay("Hamlet", 4024));
        plays.Add("as-like", new ComedyPlay("As You Like It", 2670));
        plays.Add("othello", new TragedyPlay("Othello", 3560));
        plays.Add("henry-v", new HistoricalPlay("Henry V", 3227));
        plays.Add("john", new HistoricalPlay("King John", 2648));
        plays.Add("richard-iii", new HistoricalPlay("Richard III", 3718));

        Invoice invoice = new Invoice(
            "BigCo",
            new List<Performance>
            {
                new Performance(plays["hamlet"], 55),
                new Performance(plays["as-like"], 35),
                new Performance(plays["othello"], 40),
                new Performance(plays["henry-v"], 20),
                new Performance(plays["john"], 39),
                new Performance(plays["henry-v"], 20)
            }
        );

        var xmlPrinter = new XmlStatementFormatter();
        StatementPrinter statementPrinter = new StatementPrinter(xmlPrinter);
        var result = statementPrinter.Print(invoice);

        Approvals.Verify(result);
    }
}
