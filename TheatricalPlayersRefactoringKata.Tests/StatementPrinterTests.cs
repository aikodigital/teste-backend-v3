using System;
using System.Collections.Concurrent;
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
        var plays = new List<Play>()
        {
        new TragedyPlay("Hamlet", 4024),
        new ComedyPlay("As You Like It", 2670),
        new TragedyPlay("Othello", 3560)
        };

        var performances = new List<Performance>{
            new Performance(plays[0], 55),
            new Performance(plays[1], 35),
            new Performance(plays[2], 40)
        };

        var invoice = new Invoice("BigCo", performances);
        var statementPrinter = new StatementPrinter();

        var result = statementPrinter.Print(invoice);
        Approvals.Verify(result);
    }

    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void TestTextStatementExample()
    {
        var plays = new List<Play>
        {
            new TragedyPlay("Hamlet", 4024),
            new ComedyPlay("As You Like It", 2670),
            new TragedyPlay("Othello", 3560),
            new HistoryPlay("Henry V", 3227),
            new HistoryPlay("King John", 2648),
            new HistoryPlay("Henry V", 3227)
        };

        var performances = new List<Performance>
        {
            new Performance(plays[0], 55),
            new Performance(plays[1], 35),
            new Performance(plays[2], 40),
            new Performance(plays[3], 20),
            new Performance(plays[4], 39),
            new Performance(plays[5], 20)
        };

        var invoice = new Invoice("BigCo", performances);
        var statementPrinter = new StatementPrinter();

        var result = statementPrinter.Print(invoice);
        Approvals.Verify(result);
    }

    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void TestXmlStatementExample()
    {
        var plays = new List<Play>
        {
            new TragedyPlay("Hamlet", 4024),
            new ComedyPlay("As You Like It", 2670),
            new TragedyPlay("Othello", 3560),
            new HistoryPlay("Henry V", 3227),
            new HistoryPlay("King John", 2648),
            new HistoryPlay("Henry V", 3227)
        };

        var performances = new List<Performance>
        {
            new Performance(plays[0], 55),
            new Performance(plays[1], 35),
            new Performance(plays[2], 40),
            new Performance(plays[3], 20),
            new Performance(plays[4], 39),
            new Performance(plays[5], 20)
        };

        var invoice = new Invoice("BigCo", performances);
        var xmlStatementPrinter = new XmlStatementPrinter();

        var result = xmlStatementPrinter.Print(invoice);
        Approvals.Verify(result);
    }
}
