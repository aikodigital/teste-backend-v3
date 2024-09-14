using ApprovalTests;
using ApprovalTests.Reporters;
using TheatricalPlayers.Application.Handlers;
using TheatricalPlayers.Core.Entities;
using TheatricalPlayers.Core.Enums;
using TheatricalPlayers.Core.Interfaces.Statements;
using TheatricalPlayersRefactoringKata.Tests.Application.Mocks;
using Xunit;

namespace TheatricalPlayersRefactoringKata.Tests.Application.Handlers.StatementTests;

public class PrintXml
{
    private readonly IStatementPrinterHandler _statementPrinterHandler; 
    public PrintXml()
    {
        _statementPrinterHandler = new StatementPrinterHandler();
    }

    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void PrintXmlOk()
    {
       
        var (plays, invoice) = InvoiceSampleDataGenerator.CreateSampleData();

        var xml = _statementPrinterHandler.PrintXml(invoice, plays);
        
        Approvals.Verify(xml);
    }
}