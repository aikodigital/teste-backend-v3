using System;
using System.Collections.Generic;
using Aplication.DTO;
using Aplication.Services;
using ApprovalTests;
using ApprovalTests.Reporters;
using TheatricalPlayersRefactoringKata.Entity;
using Xunit;

namespace TheatricalPlayersRefactoringKata.Tests;

public class StatementPrinterTests
{
    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void TestStatementExampleLegacy()
    {
        StatementService statementService = new();
        var invoiceBigCo = statementService.ObterInvoiceBigCo();
        var result = StatementService.Print(invoiceBigCo);
        Approvals.Verify(result);
    }

    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void TestTextStatementExample()
    {
        StatementService statementService = new();
        var invoiceBigCo = statementService.ObterInvoiceBigCo2();
        var result = StatementService.Print(invoiceBigCo);
        Approvals.Verify(result);
    }
}
