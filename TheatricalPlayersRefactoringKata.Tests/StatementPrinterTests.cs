using System.Collections.Generic;
using ApprovalTests;
using ApprovalTests.Reporters;
using TheatricalPlayersRefactoringKata.Models;
using TheatricalPlayersRefactoringKata.Presenters;
using TheatricalPlayersRefactoringKata.Views;
using Xunit;

namespace TheatricalPlayersRefactoringKata.Tests;

public class StatementPrinterTests
{
    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void TestTextStatementExample()
    {
        TragedyPlay hamlet = new TragedyPlay("Hamlet", 4024);
        ComedyPlay asLike = new ComedyPlay("As You Like It", 2670);
        TragedyPlay othello = new TragedyPlay("Othello", 3560);
        HistoryPlay henry = new HistoryPlay("Henry V", 3227);
        HistoryPlay kingJohn = new HistoryPlay("King John", 2648);
        HistoryPlay richard = new HistoryPlay("Richard III", 3718);

        Invoice invoice = new Invoice(
            "BigCo",
            new List<Performance>
            {
                new Performance(hamlet, 55),
                new Performance(asLike, 35),
                new Performance(othello, 40),
                new Performance(henry, 20),
                new Performance(kingJohn, 39),
                new Performance(henry, 20)
            }
        );

        StatementPresenter statementPresenter = new StatementPresenter(invoice);
        TextStatementPrinter textStatementPrinter = new TextStatementPrinter();
        var result = textStatementPrinter.Print(statementPresenter);

        Approvals.Verify(result);
    }
}
