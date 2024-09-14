using ApprovalTests;
using ApprovalTests.Reporters;
using TheatricalPlayers.Application.Handlers;
using TheatricalPlayers.Core.Entities;
using TheatricalPlayers.Core.Enums;
using TheatricalPlayers.Core.Interfaces.Statements;
using TheatricalPlayersRefactoringKata.Tests.Application.Mocks;
using Xunit;

namespace TheatricalPlayersRefactoringKata.Tests.Application.Handlers.StatementTests;

public class PrintTxt
{
    private readonly IStatementPrinterHandler _statementPrinterHandler;
    public PrintTxt()
    {
        _statementPrinterHandler = new StatementPrinterHandler();
    }

    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void PrintTxtOk()
    {
        var (plays, invoice) = InvoiceSampleDataGenerator.CreateSampleData();
        
        var txt = _statementPrinterHandler.PrintTxt(invoice, plays);

        Approvals.Verify(txt);
    }
}