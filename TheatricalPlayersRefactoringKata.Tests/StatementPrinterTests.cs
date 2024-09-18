using ApprovalTests;
using ApprovalTests.Reporters;
using System.Collections.Generic;
using TheatricalPlayersRefactoringKata.Application;
using TheatricalPlayersRefactoringKata.Application.Models;
using Xunit;

namespace TheatricalPlayersRefactoringKata.Tests;

public class StatementPrinterTests
{
    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void TestStatementExampleLegacy()
    {
        var plays = new Dictionary<string, PlayModel>
        {
            { "hamlet", new PlayModel("Hamlet", 4024, TypePlay.Tragedy) },
            { "as-like", new PlayModel("As You Like It", 2670, TypePlay.Comedy) },
            { "othello", new PlayModel("Othello", 3560, TypePlay.Tragedy) }
        };

        var invoice = new InvoiceModel("BigCo", new List<PerformanceModel>
        {
            new ("hamlet", 55),
            new ("as-like", 35),
            new ("othello", 40)
        });

        var statementPrinter = new StatementPrinter();
        var result = StatementPrinter.Print(invoice, plays);

        Approvals.Verify(result);
    }

    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void TestTextStatementExample()
    {
        var plays = new Dictionary<string, PlayModel>
        {
            { "hamlet", new PlayModel("Hamlet", 4024, TypePlay.Tragedy) },
            { "as-like", new PlayModel("As You Like It", 2670, TypePlay.Comedy) },
            { "othello", new PlayModel("Othello", 3560, TypePlay.Tragedy) },
            { "henry-v", new PlayModel("Henry V", 3227, TypePlay.History) },
            { "john", new PlayModel("King John", 2648, TypePlay.History) },
            { "richard-iii", new PlayModel("Richard III", 3718, TypePlay.History) }
        };

        var invoice = new InvoiceModel("BigCo", new List<PerformanceModel>
        {
            new ("hamlet", 55),
            new ("as-like", 35),
            new ("othello", 40),
            new ("henry-v", 20),
            new ("john", 39),
            new ("richard-iii", 20)
        });

        var statementPrinter = new StatementPrinter();
        var result = StatementPrinter.Print(invoice, plays);

        Approvals.Verify(result);
    }

    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void TestXmlStatementExample()
    {
        var plays = new Dictionary<string, PlayModel>
        {
            { "hamlet", new PlayModel("Hamlet", 4024, TypePlay.Tragedy) },
            { "as-like", new PlayModel("As You Like It", 2670, TypePlay.Comedy) },
            { "othello", new PlayModel("Othello", 3560, TypePlay.Tragedy) },
            { "henry-v", new PlayModel("Henry V", 3227, TypePlay.History) },
            { "john", new PlayModel("King John", 2648, TypePlay.History) },
            { "richard-iii", new PlayModel("Richard III", 3718, TypePlay.History) }
        };

        var invoice = new InvoiceModel("BigCo", new List<PerformanceModel>
        {
            new ("hamlet", 55),
            new ("as-like", 35),
            new ("othello", 40),
            new ("henry-v", 20),
            new ("john", 39),
            new ("richard-iii", 20)
        });

        var statementPrinter = new StatementPrinter();
        var result = StatementPrinter.Print(invoice, plays, ReportType.XML);

        Approvals.Verify(result);
    }
}
