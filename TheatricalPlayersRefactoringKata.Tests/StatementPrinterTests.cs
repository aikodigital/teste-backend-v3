using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ApprovalTests;
using ApprovalTests.Namers;
using ApprovalTests.Reporters;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using TheatricalPlayersRefactoringKata.Application.Services;
using TheatricalPlayersRefactoringKata.Application.UseCases;
using TheatricalPlayersRefactoringKata.Domain.Entities;
using TheatricalPlayersRefactoringKata.Domain.Interfaces;
using TheatricalPlayersRefactoringKata.Domain.Services.FormatterSelection;
using TheatricalPlayersRefactoringKata.Infrastructure.Data;
using TheatricalPlayersRefactoringKata.Infrastructure.Queues;
using TheatricalPlayersRefactoringKata.Infrastructure.Repositories;
using TheatricalPlayersRefactoringKata.Infrastructure.Services;
using TheatricalPlayersRefactoringKata.Presentation;
using Xunit;

namespace TheatricalPlayersRefactoringKata.Tests;

public class StatementPrinterTests
{
    private readonly ApplicationDbContext _context;

    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void TestStatementExampleLegacy()
    {
        var plays = new Dictionary<string, Play>
        {
            { "hamlet", new Play("Hamlet", 4024, "tragedy") },
            { "as-like", new Play("As You Like It", 2670, "comedy") },
            { "othello", new Play("Othello", 3560, "tragedy") }
        };

        var invoice = new Invoice(
            "BigCo",
            new List<Performance>
            {
                new Performance("hamlet", 55),
                new Performance("as-like", 35),
                new Performance("othello", 40),
            }
        );

        IPlayRepository playRepository = new PlayRepository(plays);
        ExtractService extractService = new ExtractService(_context);
        var generateStatementUseCase = new GenerateStatementUseCase(playRepository, extractService);
        TextStatementPrinter statementPrinter = new TextStatementPrinter();

        var statementResult = generateStatementUseCase.GenerateExtractValues(invoice);
        var result = statementPrinter.Print(statementResult);

        Approvals.Verify(result);
    }

    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void TestTextStatementExample()
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

        var invoice = new Invoice(
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

        IPlayRepository playRepository = new PlayRepository(plays);
        ExtractService extractService = new ExtractService(_context);
        var generateStatementUseCase = new GenerateStatementUseCase(playRepository, extractService);
        TextStatementPrinter statementPrinter = new TextStatementPrinter();

        var statementResult = generateStatementUseCase.GenerateExtractValues(invoice);
        var result = statementPrinter.Print(statementResult);

        Approvals.Verify(result);
    }

    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void TestXmlStatementExample()
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

        var invoice = new Invoice(
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

        IPlayRepository playRepository = new PlayRepository(plays);
        ExtractService extractService = new ExtractService(_context);
        var generateStatementUseCase = new GenerateStatementUseCase(playRepository, extractService);
        XmlStatementPrinter statementPrinterXml = new XmlStatementPrinter();

        var statementResult = generateStatementUseCase.GenerateExtractValues(invoice);
        var result = statementPrinterXml.Print(statementResult);

        Approvals.Verify(result);
    }

    [Fact]
    [UseReporter(typeof(DiffReporter))]
    // Test to verify statement generation with different format types (e.g., text, xml, etc)
    public void TestGeneralStatement() 
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

        var invoice = new Invoice(
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

        var playRepository = new PlayRepository(plays);
        ExtractService extractService = new ExtractService(_context);
        var generateStatementUseCase = new GenerateStatementUseCase(playRepository, extractService);

        // This could come from user input or configuration
        string format = "xml"; 

        // Get the format based on the user input
        IStatementFormatter formatter = FormatterFactory.GetFormatter(format);

        // Get the scenario name dynamically
        string scenarioName = FileNameTestFactory.GetApprovedFileName(format);

        // Set the scenario name for ApprovalTests
        ApprovalResults.ForScenario(scenarioName);

        var statementResult = generateStatementUseCase.GenerateExtractValues(invoice);
        var result = formatter.Print(statementResult);

        Approvals.Verify(result);
    }

    [Fact]
    public async Task TestAsynchronousStatementProcessing()
    {
        var plays = new Dictionary<string, Play>
        {
            { "hamlet", new Play("Hamlet", 4024, "tragedy") },
            { "as-like", new Play("As You Like It", 2670, "comedy") },
            { "othello", new Play("Othello", 3560, "tragedy") },
        };

        var invoice1 = new Invoice(
            "TestCustomer1",
            new List<Performance>
            {
                new Performance("hamlet", 55),
                new Performance("as-like", 35),
                new Performance("othello", 40),
            }
        );

        var invoice2 = new Invoice(
            "TestCustomer2",
            new List<Performance>
            {
                new Performance("othello", 40),
                new Performance("hamlet", 60)
            }
        );

        IPlayRepository playRepository = new PlayRepository(plays);
        ExtractService extractService = new ExtractService(_context);
        var generateStatementUseCase = new GenerateStatementUseCase(playRepository, extractService);
        var xmlFormatter = new XmlStatementPrinter();

        // Create the statement queue and enqueue the invoices
        IStatementQueue statementQueue = new StatementQueue();
        statementQueue.EnqueueStatement(invoice1);
        statementQueue.EnqueueStatement(invoice2);

        ILogger<StatementProcessingService> logger = NullLogger<StatementProcessingService>.Instance;

        var statementProcessingService = new StatementProcessingService(
            statementQueue,
            generateStatementUseCase,
            xmlFormatter,
            logger
        );

        var cts = new CancellationTokenSource();

        // Start the service
        await statementProcessingService.StartAsync(cts.Token);

        await Task.Delay(TimeSpan.FromSeconds(5));

        // Stop the service
        await statementProcessingService.StopAsync(cts.Token);
    } 
}
