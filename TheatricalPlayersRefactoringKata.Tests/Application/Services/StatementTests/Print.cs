using ApprovalTests;
using ApprovalTests.Reporters;
using TheatricalPlayers.Application.Services.Statements;
using TheatricalPlayers.Core.Entities;
using TheatricalPlayers.Core.Enums;
using TheatricalPlayers.Core.Interfaces.Statements;
using Xunit;

namespace TheatricalPlayersRefactoringKata.Tests.Application.Services.StatementTests;

public class Print
{
    private readonly IStatementPrinter _statementPrinter;
    public Print()
    {
        _statementPrinter = new StatementPrinter();
    }
    
    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void TestStatementExampleLegacy() 
    {
        var plays = new List<Play>// peças
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
           /* new()
            {
                Id = 4,
                Name = "The revolution",
                Lines = 1547,
                Type = PlayTypeEnum.Historical //todo, adicionar type, ajustar metodos e ajustar arquivo "Print.TestStatementExampleLegacy.approved"
            }*/
        };

        var invoice = new Invoice
        {
            Customer = "BigCo",
            Performances = new List<Performance> // apresentações
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
                }
            }
        };
        
        var result = _statementPrinter.Print(invoice, plays);

        Approvals.Verify(result);
    }
/*
    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void TestTextStatementExample()
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
                new Performance("hamlet", 55),
                new Performance("as-like", 35),
                new Performance("othello", 40),
                new Performance("henry-v", 20),
                new Performance("john", 39),
                new Performance("henry-v", 20)
            }
        );

        StatementPrinter statementPrinter = new StatementPrinter();
        var result = statementPrinter.Print(invoice, plays);

        Approvals.Verify(result);
    }*/
}