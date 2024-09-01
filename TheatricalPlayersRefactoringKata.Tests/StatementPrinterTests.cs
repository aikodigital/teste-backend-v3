using System.Collections.Generic;
using ApprovalTests;
using ApprovalTests.Reporters;
using Xunit;
using Main.Application.Services.StatementPrinter;
using Main.Contracts.StatementPrinter;

namespace TheatricalPlayersRefactoringKata.Tests;

public class StatementPrinterTests
{
    private readonly IStatementPrinterService _statementPrinterService;

    public StatementPrinterTests(IStatementPrinterService statementPrinterService)
    {
        _statementPrinterService = statementPrinterService;
    }

    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void TestStatementExampleLegacy()
    {
        var plays = new Dictionary<string, Play>();
        plays.Add("hamlet", new Play { Name="Hamlet", Lines=4024, Type= "tragedy" });
        plays.Add("as-like", new Play { Name="As You Like It", Lines = 2670, Type = "comedy" });
        plays.Add("othello", new Play{ Name = "Othello", Lines = 3560, Type = "tragedy" });

        Invoice invoice = new Invoice
        {
            Customer = "BigCo",
            Performances = new List<Performance>
            {
                new Performance{PlayId="hamlet", Audience=55 },
                new Performance{PlayId="as-like", Audience=35 },
                new Performance{ PlayId = "othello", Audience = 40 },
            }
        };

        var result = _statementPrinterService.Print(invoice, plays);

        Approvals.Verify(result.Result);
    }

    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void TestTextStatementExample()
    {
        var plays = new Dictionary<string, Play>();
        plays.Add("hamlet", new Play{ Name = "Hamlet", Lines = 4024, Type = "tragedy" });
        plays.Add("as-like", new Play{ Name = "As You Like It", Lines = 2670, Type = "comedy" });
        plays.Add("othello", new Play{ Name = "Othello", Lines = 3560, Type = "tragedy" }   );
        plays.Add("henry-v", new Play{ Name = "Henry V", Lines = 3227, Type = "history" });
        plays.Add("john", new Play{ Name = "King John", Lines = 2648, Type = "history" });
        plays.Add("richard-iii", new Play{ Name = "Richard III", Lines = 3718, Type = "history" });

        Invoice invoice = new Invoice
        {
            Customer = "BigCo",
            Performances = new List<Performance>
            {
                new Performance{ PlayId = "hamlet", Audience = 55 },
                new Performance{ PlayId = "as-like", Audience = 35 },
                new Performance{ PlayId = "othello", Audience = 40 },
                new Performance{ PlayId = "henry-v", Audience = 20 },
                new Performance{ PlayId = "john", Audience = 39 },
                new Performance{ PlayId = "henry-v", Audience = 20 }
            }
        };

        var result = _statementPrinterService.Print(invoice, plays);

        Approvals.Verify(result.Result);
    }
}
