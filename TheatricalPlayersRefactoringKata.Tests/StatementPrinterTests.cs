using Application.UseCases.StatementUseCase;
using ApprovalTests;
using ApprovalTests.Reporters;
using Domain.Contracts.UseCases.StatementUseCase;
using System.Collections.Generic;
using TheatricalPlayersRefactoringKata.Tests.Fixture;
using Xunit;

namespace TheatricalPlayersRefactoringKata.Tests;

public class StatementPrinterTests : IClassFixture<StatementPrinterFixture>
{
    private readonly IStatementPrinterUseCase _statementPrinterUseCase;

    public StatementPrinterTests(StatementPrinterFixture fixture)
    {
        _statementPrinterUseCase = fixture.StatementPrinterUseCase;
    }

    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void TestStatementExampleLegacy()
    {
        var plays = new Dictionary<string, Play>
        {
            { "hamlet", new("Hamlet", 4024, "tragedy") },
            { "as-like", new("As You Like It", 2670, "comedy") },
            { "othello", new("Othello", 3560, "tragedy") }
        };

        Invoice invoice = new(
            "BigCo",
            new List<Performance>
            {
                new("hamlet", 55),
                new("as-like", 35),
                new("othello", 40),
            }
        );

        var printResult = _statementPrinterUseCase.Print(invoice, plays);
        var result = _statementPrinterUseCase.ConvertJsonToTxt(printResult);

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

        Invoice invoice = new(
            "BigCo",
            new List<Performance>
            {
                new("hamlet", 55),
                new("as-like", 35),
                new("othello", 40),
                new("henry-v", 20),
                new("john", 39),
                new("henry-v", 20)
            }
        );

        var printResult = _statementPrinterUseCase.Print(invoice, plays);
        var result = _statementPrinterUseCase.ConvertJsonToTxt(printResult);

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

        Invoice invoice = new(
            "BigCo",
            new List<Performance>
            {
                new("hamlet", 55),
                new("as-like", 35),
                new("othello", 40),
                new("henry-v", 20),
                new("john", 39),
                new("henry-v", 20)
            }
        );

        var printResult = _statementPrinterUseCase.Print(invoice, plays);
        string result = _statementPrinterUseCase.ConvertJsonToXml(printResult);

        Approvals.Verify(result);
    }
}
