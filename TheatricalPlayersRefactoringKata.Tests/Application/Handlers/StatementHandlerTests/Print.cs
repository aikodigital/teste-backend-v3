using ApprovalTests;
using ApprovalTests.Reporters;
using TheatricalPlayers.Application.Handlers;
using TheatricalPlayers.Core.Entities;
using TheatricalPlayers.Core.Enums;
using TheatricalPlayers.Core.Interfaces.Statements;
using Xunit;

namespace TheatricalPlayersRefactoringKata.Tests.Application.Handlers.StatementTests;

public class Print
{
    private readonly IStatementPrinterHandler _statementPrinterHandler;
    public Print()
    {
        _statementPrinterHandler = new StatementPrinterHandler();
    }

    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void TestTextStatementExample()
    {
        var plays = new List<Play>
        {
            new()
            {
                Id = 1,
                Name = "Hamlet",
                Lines = 4024,
                Type = PlayTypeEnum.Tragedy
            },
            new()
            {
                Id = 2,
                Name = "As You Like It",
                Lines = 2670,
                Type = PlayTypeEnum.Comedy
            },
            new()
            {
                Id = 3,
                Name = "Othello",
                Lines = 3560,
                Type = PlayTypeEnum.Tragedy
            },
            new()
            {
                Id = 4,
                Name = "Henry V",
                Lines = 3227,
                Type = PlayTypeEnum.Historical
            },
            new()
            {
                Id = 5,
                Name = "john",
                Lines = 2648,
                Type = PlayTypeEnum.Historical
            },
            new()
            {
                Id = 6,
                Name = "Richard III",
                Lines = 3718,
                Type = PlayTypeEnum.Historical
            }
        };

        var invoice = new Invoice
        {
            Customer = "BigCo",
            Performances = new List<Performance>
            {
                new()
                {
                    PlayId = 1,
                    Audience = 55
                },
                new()
                {
                    PlayId = 2,
                    Audience = 35
                },
                new()
                {
                    PlayId = 3,
                    Audience = 40
                },
                new()
                {
                    PlayId = 4,
                    Audience = 20
                },
                new()
                {
                    PlayId = 5,
                    Audience = 39
                },
                new()
                {
                    PlayId = 6,
                    Audience = 20
                }
            }
        };
        
        var result = _statementPrinterHandler.Print(invoice, plays);

        Approvals.Verify(result);
    }
}