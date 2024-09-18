using ApprovalTests;
using ApprovalTests.Reporters;
using System.Collections.Generic;
using TheatricalPlayersRefactoringKata.Application.Models;
using TheatricalPlayersRefactoringKata.Application.Services.Report;
using Xunit;

namespace TheatricalPlayersRefactoringKata.Tests;

public class StatementPrinterTests
{
    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void TestStatementExampleLegacy()
    {
        var plays = new List<PlayModel>
        {
            new("Hamlet", 4024, TypePlay.Tragedy)
            {
                Id = "hamlet"
            },
            new("As You Like It", 2670, TypePlay.Comedy)
            {
                Id = "as-like"
            },
            new ("Othello", 3560, TypePlay.Tragedy)
            {
                Id = "othello"
            }
        };

        var invoice = new InvoiceModel("BigCo", new List<PerformanceModel>
        {
            new ("hamlet", 55),
            new ("as-like", 35),
            new ("othello", 40)
        });

        var result = StatementPrinter.Print(invoice, plays);

        Approvals.Verify(result);
    }

    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void TestTextStatementExample()
    {
        var plays = new List<PlayModel>
        {
            new("Hamlet", 4024, TypePlay.Tragedy)
            {
                Id = "hamlet"
            },
            new("As You Like It", 2670, TypePlay.Comedy)
            {
                Id = "as-like"
            },
            new ("Othello", 3560, TypePlay.Tragedy)
            {
                Id = "othello"
            },
            new ("Henry V", 3227, TypePlay.History)
            {
                Id = "henry-v"
            },
            new ("King John", 2648, TypePlay.History)
            {
                Id = "john"
            },
            new ("Richard III", 3718, TypePlay.History)
            {
                Id = "richard-iii"
            }
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

        var result = StatementPrinter.Print(invoice, plays);

        Approvals.Verify(result);
    }

    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void TestXmlStatementExample()
    {
        var plays = new List<PlayModel>
        {
            new("Hamlet", 4024, TypePlay.Tragedy)
            {
                Id = "hamlet"
            },
            new("As You Like It", 2670, TypePlay.Comedy)
            {
                Id = "as-like"
            },
            new ("Othello", 3560, TypePlay.Tragedy)
            {
                Id = "othello"
            },
            new ("Henry V", 3227, TypePlay.History)
            {
                Id = "henry-v"
            },
            new ("King John", 2648, TypePlay.History)
            {
                Id = "john"
            },
            new ("Richard III", 3718, TypePlay.History)
            {
                Id = "richard-iii"
            }
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

        var result = StatementPrinter.Print(invoice, plays, ReportType.XML);

        Approvals.Verify(result);
    }
}
