using ApprovalTests;
using ApprovalTests.Reporters;
using System;
using System.Collections.Generic;
using TheatricalPlayersRefactoringKata.Application.Services.Statment.Print;
using TheatricalPlayersRefactoringKata.Core.Entities;
using TheatricalPlayersRefactoringKata.Core.Enuns;
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
            { "hamlet", new Play("Hamlet", 4024, Genre.Tragedy) },
            { "as-like", new Play("As You Like It", 2670, Genre.Comedy) },
            { "othello", new Play("Othello", 3560, Genre.Tragedy) }
        };

        var invoice = new Invoice("BigCo", new List<Performance>
{
            new Performance(plays["hamlet"].Id, 55, plays["hamlet"]),
            new Performance(plays["as-like"].Id, 35, plays["as-like"]),
            new Performance(plays["othello"].Id, 40, plays["othello"])
        });


        var result = TextStatementFormatter.Print(invoice);

        Approvals.Verify(result);
    }

    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void TestTextStatementExample()
    {
        var plays = new Dictionary<string, Play>
        {
            { "hamlet", new Play("Hamlet", 4024, Genre.Tragedy) },
            { "as-like", new Play("As You Like It", 2670, Genre.Comedy) },
            { "othello", new Play("Othello", 3560, Genre.Tragedy) },
            { "henry-v", new Play("Henry V", 3227, Genre.Historical) },
            { "john", new Play("King John", 2648, Genre.Historical) },
            { "richard-iii", new Play("Richard III", 3718, Genre.Historical) }
        };

        var invoice = new Invoice("BigCo", new List<Performance>
        {
            new Performance(plays["hamlet"].Id, 55, plays["hamlet"]),
            new Performance(plays["as-like"].Id, 35, plays["as-like"]),
            new Performance(plays["othello"].Id, 40, plays["othello"]),
            new Performance(plays["henry-v"].Id, 20, plays["henry-v"]),
            new Performance(plays["john"].Id, 39, plays["john"]),
            new Performance(plays["henry-v"].Id, 20, plays["henry-v"])
        });


        var result = TextStatementFormatter.Print(invoice);

        Approvals.Verify(result);
    }

    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void TestXmlStatementExample()
    {
        var plays = new Dictionary<string, Play>
        {
            { "hamlet", new Play("Hamlet", 4024, Genre.Tragedy) },
            { "as-like", new Play("As You Like It", 2670, Genre.Comedy) },
            { "othello", new Play("Othello", 3560, Genre.Tragedy) },
            { "henry-v", new Play("Henry V", 3227, Genre.Historical) },
            { "john", new Play("King John", 2648, Genre.Historical) },
            { "richard-iii", new Play("Richard III", 3718, Genre.Historical) }
        };

        var invoice = new Invoice("BigCo", new List<Performance>
        {
            new Performance(plays["hamlet"].Id, 55, plays["hamlet"]),
            new Performance(plays["as-like"].Id, 35, plays["as-like"]),
            new Performance(plays["othello"].Id, 40, plays["othello"]),
            new Performance(plays["henry-v"].Id, 20, plays["henry-v"]),
            new Performance(plays["john"].Id, 39, plays["john"]),
            new Performance(plays["henry-v"].Id, 20, plays["henry-v"])
        });


        var result = XmlStatementFormatter.Print(invoice);

        Approvals.Verify(result);
    }
}
