using System;
using System.Collections.Generic;
using ApprovalTests;
using ApprovalTests.Reporters;
using TheatricalPlayersRefactoringKata.Contracts;
using TheatricalPlayersRefactoringKata.Performances;
using Xunit;

namespace TheatricalPlayersRefactoringKata.Tests;

public class StatementPrinterTests
{
    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void TestStatementExampleLegacy()
    {
        Invoice invoice = new Invoice(
            "BigCo",
            new List<IPerformance>
            {
                new TragedyPerformance(new Play("Hamlet", 4024), 55),
                new ComedyPerformance(new Play("As You Like It", 2670), 35),
                new TragedyPerformance(new Play("Othello", 3560), 40)
            }
        );

        StatementPrinter statementPrinter = new StatementPrinter();
        var result = statementPrinter.Print(invoice);

        Approvals.Verify(result);
    }

    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void TestTextStatementExample()
    {
        Invoice invoice = new Invoice(
            "BigCo",
            new List<IPerformance>
            {
                new TragedyPerformance(new Play("Hamlet", 4024), 55),
                new ComedyPerformance(new Play("As You Like It", 2670), 35),
                new TragedyPerformance(new Play("Othello", 3560), 40),
                new HistoryPerformance(new Play("Henry V", 3227), 20),
                new HistoryPerformance(new Play("King John", 2648), 39),
                new HistoryPerformance(new Play("Richard III", 3718), 20)
            }
        );

        StatementPrinter statementPrinter = new StatementPrinter();
        var result = statementPrinter.Print(invoice);

        Approvals.Verify(result);
    }
}
