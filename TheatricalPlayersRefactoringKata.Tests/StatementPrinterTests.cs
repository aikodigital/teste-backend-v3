#region

using System;
using System.Collections.Generic;
using ApprovalTests;
using ApprovalTests.Reporters;
using TheatricalPlayersRefactoringKata.Core.Entities;
using TheatricalPlayersRefactoringKata.Core.Printers;
using TheatricalPlayersRefactoringKata.Core.Services;
using Xunit;

#endregion

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

        var invoice = new Invoice([
            new Performance(plays["hamlet"], 55, [], new Guid()),
            new Performance(plays["as-like"], 35, [], new Guid()),
            new Performance(plays["othello"], 40, [], new Guid())
        ], "BigCo");

        var result = TextStatementPrinter.Print(invoice);

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

        var invoice = new Invoice([
            new Performance(plays["hamlet"], 55, [], new Guid()),
            new Performance(plays["as-like"], 35, [], new Guid()),
            new Performance(plays["othello"], 40, [], new Guid()),
            new Performance(plays["henry-v"], 20, [], new Guid()),
            new Performance(plays["john"], 39, [], new Guid()),
            new Performance(plays["henry-v"], 20, [], new Guid())
        ], "BigCo");

        var result = TextStatementPrinter.Print(invoice);

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

        var invoice = new Invoice([
            new Performance(plays["hamlet"], 55, [], new Guid()),
            new Performance(plays["as-like"], 35, [], new Guid()),
            new Performance(plays["othello"], 40, [], new Guid()),
            new Performance(plays["henry-v"], 20, [], new Guid()),
            new Performance(plays["john"], 39, [], new Guid()),
            new Performance(plays["henry-v"], 20, [], new Guid())
        ], "BigCo");

        var result = XmlStatementPrinter.Print(invoice);

        Approvals.Verify(result);
    }
}