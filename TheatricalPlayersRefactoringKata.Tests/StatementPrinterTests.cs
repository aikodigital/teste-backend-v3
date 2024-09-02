using System.Collections.Generic;
using ApprovalTests;
using ApprovalTests.Reporters;
using Xunit;
using Main.Application.Services.StatementPrinter;
using Main.Contracts.StatementPrinter;
using System;

namespace TheatricalPlayersRefactoringKata.Tests;

public class StatementPrinterTests
{
    private readonly Func<string, IStatementPrinterService> _statementPrinterServiceFactory;

    public StatementPrinterTests(Func<string, IStatementPrinterService> statementPrinterServiceFactory)
    {
        _statementPrinterServiceFactory = statementPrinterServiceFactory;
    }

    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void TestStatementExampleLegacy()
    {
        var plays = new Dictionary<string, Play>
        {
            { "hamlet", new Play { Name = "Hamlet", Lines = 4024, Type = "tragedy" } },
            { "as-like", new Play { Name = "As You Like It", Lines = 2670, Type = "comedy" } },
            { "othello", new Play { Name = "Othello", Lines = 3560, Type = "tragedy" } }
        };

        Invoice invoice = new Invoice
        {
            Customer = "BigCo",
            Performances = new List<Performance>
            {
                new() {PlayId="hamlet", Audience=55 },
                new() {PlayId="as-like", Audience=35 },
                new() { PlayId = "othello", Audience = 40 },
            }
        };

        var statementPrinterService = _statementPrinterServiceFactory("default");
        var result = statementPrinterService.Print(invoice, plays);
        Approvals.Verify(result.Result);
    }

    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void TestTextStatementExample()
    {
        var plays = new Dictionary<string, Play>
        {
            { "hamlet", new Play { Name = "Hamlet", Lines = 4024, Type = "tragedy" } },
            { "as-like", new Play { Name = "As You Like It", Lines = 2670, Type = "comedy" } },
            { "othello", new Play { Name = "Othello", Lines = 3560, Type = "tragedy" } },
            { "henry-v", new Play { Name = "Henry V", Lines = 3227, Type = "history" } },
            { "john", new Play { Name = "King John", Lines = 2648, Type = "history" } },
            { "richard-iii", new Play { Name = "Richard III", Lines = 3718, Type = "history" } }
        };

        Invoice invoice = new Invoice
        {
            Customer = "BigCo",
            Performances = new List<Performance>
            {
                new() { PlayId = "hamlet", Audience = 55 },
                new() { PlayId = "as-like", Audience = 35 },
                new() { PlayId = "othello", Audience = 40 },
                new() { PlayId = "henry-v", Audience = 20 },
                new() { PlayId = "john", Audience = 39 },
                new() { PlayId = "henry-v", Audience = 20 }
            }
        };

       
        var statementPrinterService = _statementPrinterServiceFactory("default");
        var result = statementPrinterService.Print(invoice, plays);
        Approvals.Verify(result.Result);
    }

    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void TestXmlStatementExample()
    {
        var plays = new Dictionary<string, Play>
        {
            { "hamlet", new Play { Name = "Hamlet", Lines = 4024, Type = "tragedy" } },
            { "as-like", new Play { Name = "As You Like It", Lines = 2670, Type = "comedy" } },
            { "othello", new Play { Name = "Othello", Lines = 3560, Type = "tragedy" } },
            { "henry-v", new Play { Name = "Henry V", Lines = 3227, Type = "history" } },
            { "john", new Play { Name = "King John", Lines = 2648, Type = "history" } },
            { "richard-iii", new Play { Name = "Richard III", Lines = 3718, Type = "history" } }
        };

        var invoice = new Invoice
        {
            Customer = "BigCo",
            Performances = new List<Performance>
            {
                new() { PlayId = "hamlet", Audience = 55 },
                new() { PlayId = "as-like", Audience = 35 },
                new() { PlayId = "othello", Audience = 40 },
                new() { PlayId = "henry-v", Audience = 20 },
                new() { PlayId = "john", Audience = 39 },
                new() { PlayId = "henry-v", Audience = 20 }
            }
        };

        var statementPrinterService = _statementPrinterServiceFactory("xml");
        var result = statementPrinterService.Print(invoice, plays);
        Approvals.Verify(result.Result);
    }
}
