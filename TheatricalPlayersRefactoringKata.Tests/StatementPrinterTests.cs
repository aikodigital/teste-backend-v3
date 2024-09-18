using System;
using System.Collections.Generic;
using System.IO;
using ApprovalTests;
using ApprovalTests.Approvers;
using ApprovalTests.Namers;
using ApprovalTests.Reporters;
using TheatricalPlayersRefactoringKata.Application.DTOs;
using TheatricalPlayersRefactoringKata.Application.UseCases;
using TheatricalPlayersRefactoringKata.Domain.Entities;
using TheatricalPlayersRefactoringKata.Domain.Interfaces;
using TheatricalPlayersRefactoringKata.Domain.Services.FormatterSelection;
using TheatricalPlayersRefactoringKata.Infrastructure.Repositories;
using TheatricalPlayersRefactoringKata.Presentation;
using Xunit;

namespace TheatricalPlayersRefactoringKata.Tests;

public class StatementPrinterTests
{
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
        var generateStatementUseCase = new GenerateStatementUseCase(playRepository);
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
        var generateStatementUseCase = new GenerateStatementUseCase(playRepository);
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
        var generateStatementUseCase = new GenerateStatementUseCase(playRepository);
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
        var generateStatementUseCase = new GenerateStatementUseCase(playRepository);

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
}
