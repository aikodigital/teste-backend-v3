using System.Collections.Generic;
using ApprovalTests;
using ApprovalTests.Reporters;
using TheatricalPlayersRefactoringKata.Application.Services;
using TheatricalPlayersRefactoringKata.Entities;
using TheatricalPlayersRefactoringKata.Enum;
using TheatricalPlayersRefactoringKata.Infrastructure.Services;
using Xunit;

namespace TheatricalPlayersRefactoringKata.Tests.Services;

public class StatementPrinterServiceTests
{
    private readonly IStatementPrinterService _sut = new StatementPrinterService();

    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void TestStatementExampleLegacy()
    {
        // Arrange
        var plays = new Dictionary<string, PlayEntity>
        {
            { "hamlet", new PlayEntity("Hamlet", 4024, PlayTypeEnum.Tragedy) },
            { "as-like", new PlayEntity("As You Like It", 2670, PlayTypeEnum.Comedy) },
            { "othello", new PlayEntity("Othello", 3560, PlayTypeEnum.Tragedy) }
        };

        var invoice = new InvoiceEntity(
            "BigCo",
            new List<PerformanceEntity>
            {
                new("hamlet", 55),
                new("as-like", 35),
                new("othello", 40),
            }
        );

        // Act
        var result = _sut.Print(invoice, plays);

        // Assert
        Approvals.Verify(result);
    }

    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void TestTextStatementExample()
    {
        // Arrange
        var plays = new Dictionary<string, PlayEntity>
        {
            { "hamlet", new PlayEntity("Hamlet", 4024, PlayTypeEnum.Tragedy) },
            { "as-like", new PlayEntity("As You Like It", 2670, PlayTypeEnum.Comedy) },
            { "othello", new PlayEntity("Othello", 3560, PlayTypeEnum.Tragedy) },
            { "henry-v", new PlayEntity("Henry V", 3227, PlayTypeEnum.History) },
            { "john", new PlayEntity("King John", 2648, PlayTypeEnum.History) },
            { "richard-iii", new PlayEntity("Richard III", 3718, PlayTypeEnum.History) }
        };

        var invoice = new InvoiceEntity(
            "BigCo",
            new List<PerformanceEntity>
            {
                new("hamlet", 55),
                new("as-like", 35),
                new("othello", 40),
                new("henry-v", 20),
                new("john", 39),
                new("henry-v", 20)
            }
        );

        // Act
        var result = _sut.Print(invoice, plays);

        // Assert
        Approvals.Verify(result);
    }
}
