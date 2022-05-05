using System;
using System.Collections.Generic;
using ApprovalTests;
using ApprovalTests.Reporters;
using TheatricalPlayersRefactoringKata.Contracts;
using TheatricalPlayersRefactoringKata.Performances;
using TheatricalPlayersRefactoringKata.Printers;
using Xunit;

namespace TheatricalPlayersRefactoringKata.Tests;

public partial class StatementPrinterTests
{
    [Theory]
    [UseReporter(typeof(DiffReporter))]
    [MemberData(nameof(GetInvoice))]
    public void TestTextStatementExample(Invoice invoice)
    {
        var statementPrinter = new TextStatementPrinter();
        var statement = new Statement(invoice);
        var result = statementPrinter.Print(statement);
        
        Approvals.Verify(result);
    }

    [Theory]
    [UseReporter(typeof(DiffReporter))]
    [MemberData(nameof(GetInvoice))]
    public void XmlTextStatementExample(Invoice invoice)
    {
        var statementPrinter = new TextStatementPrinter();
        var statement = new Statement(invoice);
        var result = statementPrinter.Print(statement);

        Approvals.Verify(result);
    }
}
