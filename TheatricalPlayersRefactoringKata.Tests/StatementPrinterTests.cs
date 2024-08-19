using System.Collections.Generic;
using ApprovalTests;
using ApprovalTests.Reporters;
using TheatricalPlayersRefactoringKata.Models;
using Xunit;

namespace TheatricalPlayersRefactoringKata.Tests;

public class StatementPrinterTests
{
    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void TestStatementExampleLegacy()
    {
        var custumer = new Customer("BigCo", 0, 0);

        var play1 = new Play("Hamlet", 4024, Enums.EPlayType.Tragedy);
        var play2 = new Play("As You Like It", 2670, Enums.EPlayType.Comedy);
        var play3 = new Play("Othello", 3560, Enums.EPlayType.Tragedy);

        var performances = new List<Performance>
        {
            new Performance(play1, 55),
            new Performance(play2, 35),
            new Performance(play3, 40)
        };

        var invoice = new Invoice(custumer, performances);

        StatementPrinter statementPrinter = new StatementPrinter();
        var result = statementPrinter.Print(invoice);

        Approvals.Verify(result);
    }

    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void TestTextStatementExample()
    {
        var custumer = new Customer("BigCo", 0, 0);

        var Hamlet = new Play("Hamlet", 4024, Enums.EPlayType.Tragedy);
        var AsYouLikeIt = new Play("As You Like It", 2670, Enums.EPlayType.Comedy);
        var Othello = new Play("Othello", 3560, Enums.EPlayType.Tragedy);
        var HenryV = new Play("Henry V", 3227, Enums.EPlayType.History);
        var KingJohn = new Play("King John", 2648, Enums.EPlayType.History);
        var RichardIII = new Play("Richard III", 3718, Enums.EPlayType.History);

        var performances = new List<Performance>
        {
            new Performance(Hamlet, 55),
            new Performance(AsYouLikeIt, 35),
            new Performance(Othello, 40),
            new Performance(HenryV, 20),
            new Performance(KingJohn, 39),
            new Performance(HenryV, 20)
        };        

        var invoice = new Invoice(custumer, performances);

        StatementPrinter statementPrinter = new StatementPrinter();
        var result = statementPrinter.Print(invoice);

        Approvals.Verify(result);
    }

    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void TestXmlStatementExample()
    {
        var custumer = new Customer("BigCo", 0, 0);

        var Hamlet = new Play("Hamlet", 4024, Enums.EPlayType.Tragedy);
        var AsYouLikeIt = new Play("As You Like It", 2670, Enums.EPlayType.Comedy);
        var Othello = new Play("Othello", 3560, Enums.EPlayType.Tragedy);
        var HenryV = new Play("Henry V", 3227, Enums.EPlayType.History);
        var KingJohn = new Play("King John", 2648, Enums.EPlayType.History);
        var RichardIII = new Play("Richard III", 3718, Enums.EPlayType.History);

        var performances = new List<Performance>
        {
            new Performance(Hamlet, 55),
            new Performance(AsYouLikeIt, 35),
            new Performance(Othello, 40),
            new Performance(HenryV, 20),
            new Performance(KingJohn, 39),
            new Performance(HenryV, 20)
        };

        var invoice = new Invoice(custumer, performances);

        StatementPrinter statementPrinter = new StatementPrinter();

        var result = statementPrinter.Printer(invoice);

        Approvals.Verify(result);
    }
}
