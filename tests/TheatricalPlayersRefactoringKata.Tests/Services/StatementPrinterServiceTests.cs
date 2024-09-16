using ApprovalTests;
using ApprovalTests.Reporters;
using TheatricalPlayersRefactoringKata.Application.Models;
using TheatricalPlayersRefactoringKata.Application.Services;
using TheatricalPlayersRefactoringKata.Entities;
using TheatricalPlayersRefactoringKata.Infrastructure.Services;
using Xunit;

namespace TheatricalPlayersRefactoringKata.Tests.Services;

public class StatementPrinterServiceTests
{
    private readonly IStatementPrinterService _sut = new StatementPrinterService();
    private readonly StatementEntity _statementFixture;

    public StatementPrinterServiceTests()
    {
        _statementFixture = new(customer: "BigCo");
        
        _statementFixture.AddItem(name: "Hamlet", amountOwed: 650m, earnedCredits: 25, seats: 55);
        _statementFixture.AddItem(name: "As You Like It", amountOwed: 547m, earnedCredits: 12, seats: 35);
        _statementFixture.AddItem(name: "Othello", amountOwed: 456m, earnedCredits: 10, seats: 40);
        _statementFixture.AddItem(name: "Henry V", amountOwed: 705.4m, earnedCredits: 0, seats: 20);
        _statementFixture.AddItem(name: "King John", amountOwed: 931.6m, earnedCredits: 9, seats: 39);
        _statementFixture.AddItem(name: "Henry V", amountOwed: 705.4m, earnedCredits: 0, seats: 20);
        
        _statementFixture.Close(amountOwed: 3995.4m, earnedCredits: 56);
    }

    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void TestTextStatementExample()
    {
        var result = _sut.Print(_statementFixture, PrintFormatEnum.Text);
        Approvals.Verify(result);
    }

    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void TestXmlStatementExample()
    {
        var result = _sut.Print(_statementFixture, PrintFormatEnum.Xml);
        Approvals.Verify(result);
    }
}