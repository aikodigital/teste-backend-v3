using ApprovalTests;
using ApprovalTests.Reporters;
using TheatricalPlayersRefactoringKata.Domain.Enums;
using TheatricalPlayersRefactoringKata.Domain.Models;
using TheatricalPlayersRefactoringKata.Infrastructure.Strategies.Exports;

namespace TheatricalPlayersRefactoringKata.Tests;


public class StatementExportStrategyTests
{
    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void TxtStatementExportStrategy_ShouldExportCorrectly()
    {
        // Arrange
        var customer = new Customer("John Doe");

        var performances = new List<Performance>
        {
            new Performance(new Play("Comedy Play", GenreEnum.Comedy, 2000), 25) { Amount = 300, Credits = 30 },
            new Performance(new Play("Tragedy Play", GenreEnum.Tragedy, 2000), 35) { Amount = 400, Credits = 40 }
        };

        var invoice = new Invoice(customer, performances);
        var statement = new TxtStatementExportStrategy();

        // Act
        var result = statement.GenerateStatement(invoice);

        // Assert
        Approvals.Verify(result);
    }

    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void XmlStatementExportStrategy_ShouldExportCorrectly()
    {
        // Arrange
        var customer = new Customer("John Doe");

        var performances = new List<Performance>
        {
            new Performance(new Play("Comedy Play", GenreEnum.Comedy, 2000), 25) { Amount = 300, Credits = 30 },
            new Performance(new Play("Tragedy Play", GenreEnum.Tragedy, 2000), 35) { Amount = 400, Credits = 40 }
        };

        var invoice = new Invoice(customer, performances);
        var statement = new XmlStatementExportStrategy();

        // Act
        var result = statement.GenerateStatement(invoice);

        // Assert
        Approvals.Verify(result);
    }
}