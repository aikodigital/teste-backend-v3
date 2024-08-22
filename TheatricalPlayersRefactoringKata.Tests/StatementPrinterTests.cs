using System;
using System.Collections.Generic;
using ApprovalTests;
using ApprovalTests.Reporters;
using TheatricalPlayersRefactoringKata.Enums;
using TheatricalPlayersRefactoringKata.Models;
using Xunit;

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

    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void TestXmlStatementExample()
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

        var result = statementPrinter.Printer(invoice);

        Approvals.Verify(result);
    }

    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void CreatePlayNullArgumentToLinesThrowArgumentException()
    {
        // arrange

        string name = "bela e a fera";
        int lines = -4;
        var type = EPlayType.Tragedy;

        // act

        var exception = Assert.Throws<ArgumentException>(() => new Play(name, lines, type));

        // assert

        Assert.Equal("Lines should be greater than 0", exception.Message);
    }

    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void CreatePlayNullArgumentToNameThrowArgumentException()
    {
        // arrange

        string name = "it";
        int lines = 1;
        var type = EPlayType.Tragedy;

        // act

        var exception = Assert.Throws<ArgumentException>(() => new Play(name, lines, type));

        // assert

        Assert.Equal("Name should be greater than 3 characters", exception.Message);
    }

    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void CreatePlayNullArgumentToTypeThrowArgumentException()
    {
        // arrange

        string name = "itAcoisa";
        int lines = 1;
        var type = new EPlayType();

        // act

        var exception = Assert.Throws<ArgumentException>(() => new Play(name, lines, type));

        // assert

        Assert.Equal("Type should be Tragedy, Comedy or History", exception.Message);
    }

    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void CreateCustomerNullArgumentToNameThrowArgumentException()
    {
        // arrange

        string name = "it";
        double credits = 1;

        // act

        var exception = Assert.Throws<ArgumentException>(() => new Customer(name, credits));

        // assert

        Assert.Equal("Name should be greater than 3 characters", exception.Message);
    }

    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void CreateCustomerNullArgumentToCreditsThrowArgumentException()
    {
        // arrange

        string name = "itAcoisa";
        int credits = -1;

        // act

        var exception = Assert.Throws<ArgumentException>(() => new Customer(name, credits));

        // assert

        Assert.Equal("Credits should be greater than 0", exception.Message);
    }

    [Fact]
    public void CreatePerformanceNullArgumentToPlayThrowArgumentException()
    {
        // arrange

        int audience = 1;

        // act

        var exception = Assert.Throws<ArgumentException>(() => new Performance(null, audience));

        // assert

        Assert.Equal("Play cannot be null", exception.Message);
    }

    [Fact]
    public void CreatePerformanceNullArgumentToAudienceThrowArgumentException()
    {
        // arrange
        var play = new Play("It A Coisa", 14, EPlayType.Tragedy);
        int audience = -1;

        // act

        var exception = Assert.Throws<ArgumentException>(() => new Performance(play, audience));

        // assert

        Assert.Equal("Audiance should be greater than 0", exception.Message);
    }

    [Fact]
    public void CreateInvoiceNullArgumentToCustomerThrowArgumentException()
    {
        // arrange
        var Hamlet = new Play("Hamlet", 4024, Enums.EPlayType.Tragedy);
        var AsYouLikeIt = new Play("As You Like It", 2670, Enums.EPlayType.Comedy);

        var performances = new List<Performance>
        {
            new Performance(Hamlet, 55),
            new Performance(AsYouLikeIt, 35)
        };

        // act

        var exception = Assert.Throws<ArgumentException>(() => new Invoice(null, performances));

        // assert

        Assert.Equal("Customer cannot be null", exception.Message);
    }


    [Fact]
    public void CreateInvoiceNullArgumentToPerformanceThrowArgumentException()
    {
        // arrange
        var customer = new Customer("Customer test", 0);

        // act

        var exception = Assert.Throws<ArgumentException>(() => new Invoice(customer, null));

        // assert

        Assert.Equal("Performance list cannot be null", exception.Message);
    }

    [Theory]
    [InlineData(2670, 35, 372)]
    public void CalcAmountBaseComedySomeInputsReturnExpectedResult(int lines, int audience, double expectedResult)
    {
        var teste = new StatementPrinter();
        var result = teste.AmountBaseComedy(lines ,audience);
        Assert.Equal(expectedResult, result);
    }

    [Theory]
    [InlineData(4024, 402.4)]
    [InlineData(3560, 356)]
    public void CalcAmountBaseTragedySomeInputsReturnExpectedResult(int lines, double expectedResult)
    {
        var teste = new StatementPrinter();
        var result = teste.AmountBaseTragedy(lines);
        Assert.Equal(expectedResult, result);
    }

    [Theory]
    [InlineData(3227, 20, 705.40)]
    [InlineData(2648, 39, 646.6)]
    public void CalcAmountBaseHistorySomeInputsReturnExpectedResult(int lines, int audience, double expectedResult)
    {
        var teste = new StatementPrinter();
        var result1 = teste.AmountBaseTragedy(lines);
        var result2 = teste.AmountBaseComedy(lines, audience);
        var result = result1 + result2;
        Assert.Equal(expectedResult, result);
    }

    [Theory]
    [InlineData(40, 100)]
    [InlineData(152, 1220)]
    public void CalcAdditionalAmountTragedyOver30SomeInputsReturnExpectedResult(int audience, double expectedResult)
    {
        var teste = new StatementPrinter();
        var result = teste.AdditionalAmountTragedyOver30(audience);
        Assert.Equal(expectedResult, result);
    }

    [Theory]
    [InlineData(78, 390)]
    [InlineData(2579, 12895)]
    public void CalcAdditionalAmountComedyOver20SomeInputsReturnExpectedResult(int audience, double expectedResult)
    {
        var teste = new StatementPrinter();
        var result = teste.AdditionalAmountComedyOver20(audience);
        Assert.Equal(expectedResult, result);
    }

    [Theory]
    [InlineData(31, 1)]
    [InlineData(35, 5)]
    public void AddCreditsSomeInputsReturnExpectedResult(int audience, double expectedResult)
    {
        var teste = new StatementPrinter();
        var result = teste.AddCredits(audience);
        Assert.Equal(expectedResult, result);
    }

    [Theory]
    [InlineData(66, 13)]
    [InlineData(77, 15)]
    public void AddBonusCreditsSomeInputsReturnExpectedResult(int audience, double expectedResult)
    {
        var teste = new StatementPrinter();
        var result = teste.AddBonusCredit(audience);
        Assert.Equal(expectedResult, result);
    }

    [Fact]
    public void ComedyCalcReturnExpectedResult()
    {
        // arrange

        var customer = new Customer("test customer", 0);
        var play = new Play("O Paió", 2670, Enums.EPlayType.Comedy);
        var performance = new Performance(play, 35);
        var performances = new List<Performance>();
        performances.Add(performance);
        var invoice = new Invoice(customer, performances);

        var teste = new StatementPrinter();

        // Act
        
        var result = teste.ComedyCalc(0, play.Lines, performance.Audience, invoice);
        var expectedResult = 547;
        
        // Assert

        Assert.Equal(expectedResult, result);
    }

    [Fact]
    public void TragedyCalcReturnExpectedResult()
    {
        // arrange

        var customer = new Customer("Aiko", 0);
        var play = new Play("Cats", 8956, Enums.EPlayType.Tragedy);
        var performance = new Performance(play, 74);
        var performances = new List<Performance>();
        performances.Add(performance);
        var invoice = new Invoice(customer, performances);

        var teste = new StatementPrinter();

        // Act

        var result = teste.TragedyCalc(0, play.Lines, performance.Audience, invoice);
        var expectedResult = 1335.6;

        // Assert

        Assert.Equal(expectedResult, result);
    }

    [Fact]
    public void HistoryCalcReturnExpectedResult()
    {
        // arrange

        var customer = new Customer("history test", 0);
        var play = new Play("Cats", 100, Enums.EPlayType.History);
        var performance = new Performance(play, 31);
        var performances = new List<Performance>();
        performances.Add(performance);
        var invoice = new Invoice(customer, performances);       

        var teste = new StatementPrinter();

        var customer2 = new Customer("Pelé", 0);
        var play2 = new Play("MammaMia", 200, Enums.EPlayType.History);
        var performance2 = new Performance(play2, 32);
        var performances2 = new List<Performance>();
        performances2.Add(performance2);
        var invoice2 = new Invoice(customer2, performances2);

        var teste2 = new StatementPrinter();
        // Act

        var result = teste.HistoryCalc(0, play.Lines, performance.Audience, invoice);
        var result2 = teste2.HistoryCalc(0, play2.Lines, performance2.Audience, invoice2);
        var expectedResult = 278 + 316;
        var resultTotal = result + result2;
        // Assert

        Assert.Equal(expectedResult, resultTotal);
    }
}

//var custumer = new Customer("BigCo", 0);

//var Hamlet = new Play("Hamlet", 4024, Enums.EPlayType.Tragedy);
//var AsYouLikeIt = new Play("As You Like It", 2670, Enums.EPlayType.Comedy);
//var Othello = new Play("Othello", 3560, Enums.EPlayType.Tragedy);
//var HenryV = new Play("Henry V", 3227, Enums.EPlayType.History);
//var KingJohn = new Play("King John", 2648, Enums.EPlayType.History);
//var RichardIII = new Play("Richard III", 3718, Enums.EPlayType.History);

//var performances = new List<Performance>
//        {
//            new Performance(Hamlet, 55),
//            new Performance(AsYouLikeIt, 35),
//            new Performance(Othello, 40),
//            new Performance(HenryV, 20),
//            new Performance(KingJohn, 39),
//            new Performance(HenryV, 20)
//        };
