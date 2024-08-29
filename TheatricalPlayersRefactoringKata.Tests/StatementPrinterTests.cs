using ApprovalTests;
using ApprovalTests.Reporters;
using System.Collections.Generic;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Application.Services;
using TheatricalPlayersRefactoringKata.Application.Services.Gender;
using TheatricalPlayersRefactoringKata.Application.Services.Statment;
using TheatricalPlayersRefactoringKata.Core.Entities;
using Xunit;

namespace TheatricalPlayersRefactoringKata.Tests;

public class StatementPrinterTests
{
    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public async Task TestStatementExampleLegacy()
    {
        var plays = new Dictionary<string, Play>();
        plays.Add("hamlet", new Play("Hamlet", 4024, "tragedy"));
        plays.Add("as-like", new Play("As You Like It", 2670, "comedy"));
        plays.Add("othello", new Play("Othello", 3560, "tragedy"));

        Invoice invoice = new Invoice(
            "BigCo",
            new List<Performance>
            {
                new Performance("hamlet", 55, plays["hamlet"]),
                new Performance("as-like", 35, plays["as-like"]),
                new Performance("othello", 40, plays["othello"]),
            }
        );

        // Adicionar os dicionários de calculadoras
        var calculators = new Dictionary<string, IPerformanceCalculator>
        {
            { "tragedy", new TragedyCalculator() },
            { "comedy", new ComedyCalculator() },
            { "history", new HistoryCalculator() }
        };

        StatementPrinter statementPrinter = new StatementPrinter(new InvoiceCalculationService(calculators), calculators);
        var result = await statementPrinter.PrintAsync(invoice);

        Approvals.Verify(result);
    }

    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public async Task TestTextStatementExample()
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
                new Performance("hamlet", 55, plays["hamlet"]),
                new Performance("as-like", 35, plays["as-like"]),
                new Performance("othello", 40, plays["othello"]),
                new Performance("henry-v", 20, plays["henry-v"]),
                new Performance("john", 39, plays["john"]),
                new Performance("richard-iii", 20, plays["richard-iii"])
            }
        );

        // Adicionar os dicionários de calculadoras
        var calculators = new Dictionary<string, IPerformanceCalculator>
        {
            { "tragedy", new TragedyCalculator() },
            { "comedy", new ComedyCalculator() },
            { "history", new HistoryCalculator() }
        };

        StatementPrinter statementPrinter = new StatementPrinter(new InvoiceCalculationService(calculators), calculators);
        var result = await statementPrinter.PrintAsync(invoice);

        Approvals.Verify(result);
    }
}
