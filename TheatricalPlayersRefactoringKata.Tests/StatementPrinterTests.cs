using System;
using System.Collections.Generic;
using System.Linq;
using ApprovalTests;
using ApprovalTests.Reporters;
using Xunit;

namespace TheatricalPlayersRefactoringKata.Tests;

public class StatementPrinterTests
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
        { Hamlet, new Play("Hamlet", 4024, "tragedy") },
        { AsLike, new Play("As You Like It", 2670, "comedy") },
        { Othello, new Play("Othello", 3560, "tragedy") },
        { HenryV, new Play("Henry V", 3227, "history") },
        { John, new Play("King John", 2648, "history") },
        { RichardIii, new Play("Richard III", 3718, "history") }
    };

    private readonly static List<Performance> Performances = new()
    {
        new (Hamlet, 55),
        new (AsLike, 35),
        new (Othello, 40),
        new (HenryV, 20),
        new (John, 39),
        new (RichardIii, 20)
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

        var result = new StatementPrinter().Print(
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

        var result = new StatementPrinter().Print(invoice, PlayMap);

        Approvals.Verify(result);
    }
}
