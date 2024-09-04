using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApprovalTests;
using ApprovalTests.Reporters;
using TheatricalPlayersRefactoringKata.Entities;
using TheatricalPlayersRefactoringKata.Services;
using Xunit;

namespace TheatricalPlayersRefactoringKata.Tests;

public class StatementPrinterTests
{
    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public async Task TestStatementExampleLegacy()
    {
        var plays = new Dictionary<string, Play>
            {
                { "hamlet", new Play("Hamlet", 4024, "tragedy") },
                { "as-like", new Play("As You Like It", 2670, "comedy") },
                { "othello", new Play("Othello", 3560, "tragedy") }
            };

        Invoice invoice = new Invoice(
            "BigCo",
            new List<Performance>
            {
                    new Performance("hamlet", 55),
                    new Performance("as-like", 35),
                    new Performance("othello", 40),
            }
        );

        StatementPrinterService statementPrinter = new StatementPrinterService();
        var result = await statementPrinter.Print(invoice, plays);

        Approvals.Verify(result);
    }

    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public async Task TestTextStatementExample()
    {
        var plays = new Dictionary<string, Play>
        {
            { "hamlet", new Play("Hamlet", 4024, "tragedy") },
            { "as-like", new Play("As You Like It", 2670, "comedy") },
            { "othello", new Play("Othello", 3560, "tragedy") },
            { "henry-v", new Play("Henry V", 3227, "history") },
            { "john", new Play("King John", 2648, "history") }
        };

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

        StatementPrinterService statementPrinter = new StatementPrinterService();
        var result = await statementPrinter.Print(invoice, plays);

        Approvals.Verify(result);
    }

    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public async Task TestXmlStatementExample()
    {
        var plays = new Dictionary<string, Play>
        {
            { "hamlet", new Play("Hamlet", 4024, "tragedy") },
            { "as-like", new Play("As You Like It", 2670, "comedy") },
            { "othello", new Play("Othello", 3560, "tragedy") },
            { "henry-v", new Play("Henry V", 3227, "history") },
            { "john", new Play("King John", 2648, "history") },
            { "richard-iii", new Play("Richard III", 3718, "history") }
        };

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

        StatementPrinterService statementPrinter = new StatementPrinterService();
        var result = await statementPrinter.PrintAsXml(invoice, plays);

        Approvals.VerifyXml(result);
    }

}
