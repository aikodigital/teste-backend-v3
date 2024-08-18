using System;
using System.Collections.Generic;
using ApprovalTests;
using ApprovalTests.Reporters;
using ApprovalUtilities.Utilities;
using TheatricalPlayersRefactoringKata.Models;
using Xunit;
using TheatricalPlayersRefactoringKata;

namespace TheatricalPlayersRefactoringKata.Tests;

public class StatementPrinterTests
{
    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void TestStatementExampleLegacy()
    {
        var custumer = new Customer("BigCo", 0);

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

    //var plays = new Dictionary<string, Play>();
    //plays.Add("hamlet", new Play("Hamlet", 4024, "tragedy"));
    //plays.Add("as-like", new Play("As You Like It", 2670, "comedy"));
    //plays.Add("othello", new Play("Othello", 3560, "tragedy"));

    //Invoice invoice = new Invoice(custumer,new List<Performance>
    //    {
    //        new Performance("hamlet", 55),
    //        new Performance("as-like", 35),
    //        new Performance("othello", 40),
    //    }
    //);

    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void TestTextStatementExample()
    {
        var custumer = new Customer("BigCo", 0);

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

    //Invoice invoice = new Invoice(
    //    "BigCo",
    //    new List<Performance>
    //    {
    //        new Performance("hamlet", 55),
    //        new Performance("as-like", 35),
    //        new Performance("othello", 40),
    //        new Performance("henry-v", 20),
    //        new Performance("john", 39),
    //        new Performance("henry-v", 20)
    //    }
    //);

    //var plays = new Dictionary<string, Play>();
    //plays.Add("hamlet", new Play("Hamlet", 4024, "tragedy"));
    //plays.Add("as-like", new Play("As You Like It", 2670, "comedy"));
    //plays.Add("othello", new Play("Othello", 3560, "tragedy"));
    //plays.Add("henry-v", new Play("Henry V", 3227, "history"));
    //plays.Add("john", new Play("King John", 2648, "history"));
    //plays.Add("richard-iii", new Play("Richard III", 3718, "history"));
}
