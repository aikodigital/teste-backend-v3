using Aplication.Services;
using Aplication.Services.Formatters;
using ApprovalTests;
using ApprovalTests.Reporters;
using Xunit;

namespace TheatricalPlayersRefactoringKata.Tests;

public class StatementPrinterTests
{

    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void TestXmlStatementExample()
    {
        StatementService statementService = new();
        var invoiceBigCo2 = statementService.ObterInvoiceBigCo2();
        var formatter = new XmlInvoiceFormatter();
        var result = StatementService.Print(invoiceBigCo2, formatter);
        Approvals.Verify(result);
    }

    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void TestStatementExampleLegacy()
    {
        StatementService statementService = new();
        var invoiceBigCo = statementService.ObterInvoiceBigCo();
        var formatter = new TextoInvoiceFormater();
        var result = StatementService.Print(invoiceBigCo,formatter);
        Approvals.Verify(result);
    }

    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void TestTextStatementExample()
    {
        StatementService statementService = new();
        var invoiceBigCo2 = statementService.ObterInvoiceBigCo2();
        var formatter = new TextoInvoiceFormater();
        var result = StatementService.Print(invoiceBigCo2, formatter);
        Approvals.Verify(result);
    }
}
