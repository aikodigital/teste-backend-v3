using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using ApprovalTests;
using ApprovalTests.Reporters;
using Xunit;

namespace TheatricalPlayersRefactoringKata.Tests;

public class StatementPrinterTests
{
    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void TestStatementExampleLegacy()
    {
        //Arrange

        var plays = new Dictionary<string, Play>();
        plays.Add("hamlet", new Play("Hamlet", 4024, "tragedy"));
        plays.Add("as-like", new Play("As You Like It", 2670, "comedy"));
        plays.Add("othello", new Play("Othello", 3560, "tragedy"));

        Invoice invoice = new Invoice(
            "BigCo",
            new List<Performance>
            {
                new Performance("hamlet", 55),
                new Performance("as-like", 35),
                new Performance("othello", 40),
            }
        );

        StatementPrinter statementPrinter = new StatementPrinter();


        //Act
        var result = statementPrinter.Print(invoice, plays);


        //Assert
        Approvals.Verify(result);
    }

    //[Fact]
    //[UseReporter(typeof(DiffReporter))]
    //public void TestTextStatementExample()
    //{
    //    var plays = new Dictionary<string, Play>();
    //    plays.Add("hamlet", new Play("Hamlet", 4024, "tragedy"));
    //    plays.Add("as-like", new Play("As You Like It", 2670, "comedy"));
    //    plays.Add("othello", new Play("Othello", 3560, "tragedy"));
    //    plays.Add("henry-v", new Play("Henry V", 3227, "history"));
    //    plays.Add("john", new Play("King John", 2648, "history"));
    //    plays.Add("richard-iii", new Play("Richard III", 3718, "history"));

    //    Invoice invoice = new Invoice(
    //        "BigCo",
    //        new List<Performance>
    //        {
    //            new Performance("hamlet", 55),
    //            new Performance("as-like", 35),
    //            new Performance("othello", 40),
    //            new Performance("henry-v", 20),
    //            new Performance("john", 39),
    //            new Performance("henry-v", 20)
    //        }
    //    );

    //    StatementPrinter statementPrinter = new StatementPrinter();
    //    var result = statementPrinter.Print(invoice, plays);

    //    Approvals.Verify(result);
    //}

    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void TestStatementExampleLegacy_2()
    {
        //Arrange
        var plays = new Dictionary<string, Play>();
        plays.Add("hamlet", new Play("Hamlet", 4024, "tragedy"));
        plays.Add("as-like", new Play("As You Like It", 2670, "comedy"));
        plays.Add("othello", new Play("Othello", 3560, "tragedy"));

        Invoice invoice = new Invoice(
            "BigCo",
            new List<Performance>
            {
                new Performance("hamlet", 55),
                new Performance("as-like", 35),
                new Performance("othello", 40),
            }
        );

        StatementPrinter statementPrinter = new StatementPrinter();

        //Act
        statementPrinter.Print(invoice, plays);

        //Assert
        Assert.Equal(650, invoice.PerformancesAmountCurtumer["Hamlet"]);
        Assert.Equal(547, invoice.PerformancesAmountCurtumer["As You Like It"]);
        Assert.Equal(456, invoice.PerformancesAmountCurtumer["Othello"]);

        Assert.Equal(1653, invoice.TotalAmount);
        Assert.Equal(47, invoice.VolumeCredits);
    }

    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void PlayWithRangeGreater4000_Return4000()
    {
        //Arrange
        var plays = new Dictionary<string, Play>();
        plays.Add("hamlet", new Play("Hamlet", 4024, "tragedy"));

        Invoice invoice = new Invoice(
            "BigCo",
            new List<Performance>
            {
                new Performance("hamlet", 55)
            }
        );

        StatementPrinter statementPrinter = new StatementPrinter();

        //Act
        statementPrinter.Print(invoice, plays);

        //Assert
        Assert.Equal(650, invoice.PerformancesAmountCurtumer["Hamlet"]);

        Assert.Equal(650, invoice.TotalAmount);
        Assert.Equal(25, invoice.VolumeCredits);
    }

    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void PlayWithRangeLess1000_Return1000()
    {
        //Arrange
        var plays = new Dictionary<string, Play>();
        plays.Add("as-like", new Play("As You Like It", 900, "comedy"));

        Invoice invoice = new Invoice(
            "BigCo",
            new List<Performance>
            {
                new Performance("as-like", 35)
            }
        );

        StatementPrinter statementPrinter = new StatementPrinter();

        //Act
        statementPrinter.Print(invoice, plays);

        //Assert
        Assert.Equal(380, invoice.PerformancesAmountCurtumer["As You Like It"]);

        Assert.Equal(380, invoice.TotalAmount);
        Assert.Equal(12, invoice.VolumeCredits);
    }

    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void ValueBaseIsLinesDivided10()
    {
        //Arrange
        const int LINES = 4000;

        var plays = new Dictionary<string, Play>();
        plays.Add("hamlet", new Play("Hamlet", LINES, "tragedy"));

        Invoice invoice = new Invoice(
            "BigCo",
            new List<Performance>
            {
                new Performance("hamlet", 29)
            }
        );

        StatementPrinter statementPrinter = new StatementPrinter();

        //Act
        statementPrinter.Print(invoice, plays);

        //Assert
        Assert.Equal(LINES/10, invoice.PerformancesAmountCurtumer["Hamlet"]);

        Assert.Equal(400, invoice.TotalAmount);
        Assert.Equal(0, invoice.VolumeCredits);
    }

    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void TragedyPlay_WhenAudienceLessOrEqualTo30_AmountPlayWillBeBaseValue()
    {
        //Arrange

        //Act

        //Assert
    }

    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void TragedyPlay_WhenAudienceGreaterTo30_Sum10BaseValue()
    {
        //Arrange

        //Act

        //Assert
    }

}
