using Application.UseCases.StatementUseCase;
using ApprovalTests;
using ApprovalTests.Reporters;
using Domain.Contracts.UseCases.StatementUseCase;
using Domain.Entities;
using System.Collections.Generic;
using TheatricalPlayersRefactoringKata.Tests.Fixture;
using Xunit;

namespace TheatricalPlayersRefactoringKata.Tests;

public class StatementPrinterTests : IClassFixture<StatementPrinterFixture>
{
    private readonly IStatementPrinterUseCase _statementPrinterUseCase;
    private readonly IConvertUseCase _convertUseCase;

    public StatementPrinterTests(StatementPrinterFixture fixture)
    {
        _statementPrinterUseCase = fixture.StatementPrinterUseCase;
        _convertUseCase = fixture.ConvertUseCase;
    }

    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void TestStatementExampleLegacy()
    {
        var plays = new List<Play>
        {
            { new("Hamlet", "hamlet", 4024, "tragedy") },
            { new("As You Like It", "as-like", 2670, "comedy") },
            { new("Othello", "othello", 3560, "tragedy") }
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
        var result = _convertUseCase.ConvertJsonToTxt(printResult);

        Approvals.Verify(result);
    }

    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void TestTextStatementExample()
    {
        var plays = new List<Play>
        {
            { new("Hamlet", "hamlet", 4024, "tragedy") },
            { new("As You Like It", "as-like", 2670, "comedy") },
            { new("Othello", "othello", 3560, "tragedy") },
            { new("Henry V", "henry-v", 3227, "history") },
            { new("King John", "john", 2648, "history") },
            { new("Richard III", "henry-v",3718, "history") }
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
        var result = _convertUseCase.ConvertJsonToTxt(printResult);

        Approvals.Verify(result);
    }

    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void TestXmlStatementExample()
    {
        var plays = new List<Play>
        {
            { new("Hamlet", "hamlet", 4024, "tragedy") },
            { new("As You Like It", "as-like", 2670, "comedy") },
            { new("Othello", "othello", 3560, "tragedy") },
            { new("Henry V", "henry-v", 3227, "history") },
            { new("King John", "john", 2648, "history") },
            { new("Richard III", "henry-v",3718, "history") }
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
        string result = _convertUseCase.ConvertJsonToXml(printResult);

        Approvals.Verify(result);
    }
}
