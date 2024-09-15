using System;
using System.Collections.Generic;
using ApprovalTests;
using ApprovalTests.Reporters;
using TheatricalPlayersRefactoringKata.Application;
using TheatricalPlayersRefactoringKata.Domain;
using TheatricalPlayersRefactoringKata.Domain.Enums;
using Xunit;

namespace TheatricalPlayersRefactoringKata.Tests.UseCases;

public class StatementPrinterTests
{
    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void TestStatementExampleLegacy()
    {
        var hamletPlay = new Play("Hamlet", 4024, PlayTypeEnum.Tragedy);
        var asYLikePlay = new Play("As You Like It", 2670, PlayTypeEnum.Comedy);
        var othelloPlay = new Play("Othello", 3560, PlayTypeEnum.Tragedy);

        Invoice invoice = new Invoice(
            "BigCo",
            new List<Performance>
            {
                new Performance(hamletPlay, 55),
                new Performance(asYLikePlay, 35),
                new Performance(othelloPlay, 40),
            }
        );

        var statementPrinter = new StatementPrinter();
        var result = statementPrinter.Print(invoice);

        Approvals.Verify(result);
    }

    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void TestTextStatementExample()
    {
        var hamletPlay = new Play("Hamlet", 4024, PlayTypeEnum.Tragedy);
        var AsYLikeItPlay = new Play("As You Like It", 2670, PlayTypeEnum.Comedy);
        var othelloPlay = new Play("Othello", 3560, PlayTypeEnum.Tragedy);
        var henryVPlay = new Play("Henry V", 3227, PlayTypeEnum.History);
        var kingJohnPlay = new Play("King John", 2648, PlayTypeEnum.History);
        var richardIIIPlay = new Play("Richard III", 3718, PlayTypeEnum.History);

        Invoice invoice = new Invoice(
            "BigCo",
            new List<Performance>
            {
                new Performance(hamletPlay, 55),
                new Performance(AsYLikeItPlay, 35),
                new Performance(othelloPlay, 40),
                new Performance(henryVPlay, 20),
                new Performance(kingJohnPlay, 39),
                new Performance(henryVPlay, 20)
            }
        );

        var statementPrinter = new StatementPrinter();
        var result = statementPrinter.Print(invoice);

        Approvals.Verify(result);
    }

    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void TesteTextStatmentNoPerformanceExample()
    {
        Invoice invoice = new Invoice("BigCo", new List<Performance>());

        var statementPrinter = new StatementPrinter();

        var result = statementPrinter.Print(invoice);

        Approvals.Verify(result);
    }
}
