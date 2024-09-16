using ApprovalTests;
using ApprovalTests.Reporters;
using TheatricalPlayers.Application.Handlers;
using TheatricalPlayers.Core.Enums;
using TheatricalPlayers.Core.Interfaces.Statements;
using TheatricalPlayers.Tests.Application.Mocks;
using Xunit;

namespace TheatricalPlayers.Tests.Application.Handlers.StatementHandlerTests;

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