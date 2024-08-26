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
        plays.Add("hamlet", new Play("Hamlet", 4024, "tragedy"));
        plays.Add("as-like", new Play("As You Like It", 2670, "comedy"));
        plays.Add("othello", new Play("Othello", 3560, "tragedy"));

        Invoice invoice = new Invoice(
            "BigCo",
            new List<Performance>
            {
                new Performance("hamlet", 55),
                new Performance("as-like", 35),
                new Performance("othello", 40),
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
        plays.Add("hamlet", new Play("Hamlet", 4024, "tragedy"));
        plays.Add("as-like", new Play("As You Like It", 2670, "comedy"));
        plays.Add("othello", new Play("Othello", 3560, "tragedy"));
        plays.Add("henry-v", new Play("Henry V", 3227, "history"));
        plays.Add("john", new Play("King John", 2648, "history"));
        plays.Add("richard-iii", new Play("Richard III", 3718, "history"));

        Invoice invoice = new Invoice(
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

        StatementPrinter statementPrinter = new StatementPrinter();
        var result = statementPrinter.Print(invoice, plays);

        Approvals.Verify(result);
    }

    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void TestTragedyStatement()
    {
        var plays = new Dictionary<string, Play>
    {
        { "hamlet", new Play("Hamlet", 4024, "tragedy") }
    };

        Invoice invoice = new Invoice(
            "BigCo",
            new List<Performance>
            {
            new Performance("hamlet", 55)
            }
        );

        StatementPrinter statementPrinter = new StatementPrinter();
        var result = statementPrinter.Print(invoice, plays);

        Approvals.Verify(result);
    }

    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void TestComedyStatement()
    {
        var plays = new Dictionary<string, Play>
            {
                { "as-like", new Play("As You Like It", 2670, "comedy") }
            };

        Invoice invoice = new Invoice(
            "BigCo",
            new List<Performance>
            {
                    new Performance("as-like", 35)
            }
        );

        StatementPrinter statementPrinter = new StatementPrinter();
        var result = statementPrinter.Print(invoice, plays);

        Approvals.Verify(result);
    }

    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void TestHistoryStatement()
    {
        var plays = new Dictionary<string, Play>
    {
        { "henry-v", new Play("Henry V", 3227, "history") }
    };

        Invoice invoice = new Invoice(
            "BigCo",
            new List<Performance>
            {
            new Performance("henry-v", 39)
            }
        );

        StatementPrinter statementPrinter = new StatementPrinter();
        var result = statementPrinter.Print(invoice, plays);

        Approvals.Verify(result);
    }

    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void TestCurrencyFormatting()
    {
        var plays = new Dictionary<string, Play>
    {
        { "hamlet", new Play("Hamlet", 4024, "tragedy") },
        { "as-like", new Play("As You Like It", 2670, "comedy") }
    };

        Invoice invoice = new Invoice(
            "BigCo",
            new List<Performance>
            {
            new Performance("hamlet", 55),
            new Performance("as-like", 35)
            }
        );

        StatementPrinter statementPrinter = new StatementPrinter();
        var result = statementPrinter.Print(invoice, plays);

        Approvals.Verify(result);
    }

    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void TestMinMaxAudience()
    {
        var plays = new Dictionary<string, Play>
    {
        { "hamlet", new Play("Hamlet", 4024, "tragedy") }
    };

        Invoice invoice = new Invoice(
            "BigCo",
            new List<Performance>
            {
            new Performance("hamlet", 0),
            new Performance("hamlet", 1000)
            }
        );

        StatementPrinter statementPrinter = new StatementPrinter();
        var result = statementPrinter.Print(invoice, plays);

        Approvals.Verify(result);
    }
    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void TestTragedyAmountBoundaryValues()
    {
        var plays = new Dictionary<string, Play>
    {
        { "hamlet", new Play("Hamlet", 999, "tragedy") },
        { "othello", new Play("Othello", 4001, "tragedy") }
    };

        var invoice = new Invoice(
            "BigCo",
            new List<Performance>
            {
            new Performance("hamlet", 55),
            new Performance("othello", 40)
            }
        );

        var statementPrinter = new StatementPrinter();
        var result = statementPrinter.Print(invoice, plays);

        Approvals.Verify(result);
    }
    
    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void TestPrintXml()
    {
        var plays = new Dictionary<string, Play>
    {
        { "hamlet", new Play("Hamlet", 4024, "tragedy") },
        { "as-like", new Play("As You Like It", 2670, "comedy") }
    };

        Invoice invoice = new Invoice(
            "BigCo",
            new List<Performance>
            {
            new Performance("hamlet", 55),
            new Performance("as-like", 35)
            }
        );

        StatementPrinter statementPrinter = new StatementPrinter();
        var result = statementPrinter.PrintXml(invoice, plays);

        Approvals.Verify(result);
    }
}
