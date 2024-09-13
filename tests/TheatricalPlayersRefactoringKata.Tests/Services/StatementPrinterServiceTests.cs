using System.Collections.Generic;
using ApprovalTests;
using ApprovalTests.Reporters;
using TheatricalPlayersRefactoringKata.Application.Services;
using TheatricalPlayersRefactoringKata.Entities;
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
            { "hamlet", new PlayEntity("Hamlet", 4024, "tragedy") },
            { "as-like", new PlayEntity("As You Like It", 2670, "comedy") },
            { "othello", new PlayEntity("Othello", 3560, "tragedy") }
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
            { "hamlet", new PlayEntity("Hamlet", 4024, "tragedy") },
            { "as-like", new PlayEntity("As You Like It", 2670, "comedy") },
            { "othello", new PlayEntity("Othello", 3560, "tragedy") },
            { "henry-v", new PlayEntity("Henry V", 3227, "history") },
            { "john", new PlayEntity("King John", 2648, "history") },
            { "richard-iii", new PlayEntity("Richard III", 3718, "history") }
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
