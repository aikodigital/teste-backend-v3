using System.Collections.Generic;
using System.Linq;
using ApprovalTests;
using ApprovalTests.Reporters;
using TheatricalPlayersRefactoringKata.Entities;
using Xunit;

namespace TheatricalPlayersRefactoringKata.Tests.Service;

public class StatementPrinterServiceTests
{
    private readonly static string Hamlet = "hamlet";
    private readonly static string AsLike = "as-like";
    private readonly static string Othello = "othello";
    private readonly static string HenryV = "henry-v";
    private readonly static string John = "john";
    private readonly static string RichardIii = "richard-iii";
    private readonly static string Customer = "BigCo";

    private readonly static Dictionary<string, Play> PlayMap = new()
    {
        { Hamlet, new Play() { Name = "Hamlet", Lines = 4024, Type = "tragedy" } },
        { AsLike, new Play() { Name = "As You Like It", Lines = 2670, Type = "comedy" } },
        { Othello, new Play() { Name = "Othello", Lines = 3560, Type = "tragedy" } },
        { HenryV, new Play() { Name = "Henry V", Lines = 3227, Type = "history" } },
        { John, new Play() { Name = "King John", Lines = 2648, Type = "history" } },
        { RichardIii, new Play() { Name = "Richard III", Lines = 3718, Type = "history" } }
    };

    private readonly static List<Performance> Performances = new()
    {
        new () { PlayId = Hamlet, Audience = 55 },
        new () { PlayId = AsLike, Audience = 35 },
        new () { PlayId = Othello, Audience = 40 },
        new () { PlayId = HenryV, Audience = 20 },
        new () { PlayId = John, Audience = 39 },
        new () { PlayId = RichardIii, Audience = 20 }
    };


    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void TestStatementExampleLegacy()
    {
        var invoice = new Invoice
        {
            Customer = Customer,
            Performances = Performances
                .Take(3)
                .ToList()
        };

        var result = new StatementPrinterService().Print(
            invoice,
            PlayMap
                .Take(3)
                .ToDictionary(p => p.Key, p => p.Value));

        Approvals.Verify(result);
    }

    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void TestTextStatementExample()
    {
        var invoice = new Invoice
        {
            Customer = Customer,
            Performances = Performances
        };

        var result = new StatementPrinterService().Print(invoice, PlayMap);

        Approvals.Verify(result);
    }
}
