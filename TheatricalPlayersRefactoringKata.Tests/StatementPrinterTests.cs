using System;
using System.Collections.Generic;
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
        var plays = new Dictionary<string, Play>();
        plays.Add("hamlet", new Play("Hamlet", 4024, new Type("tragedy")));
        plays.Add("as-like", new Play("As You Like It", 2670, new Type("comedy")));
        plays.Add("othello", new Play("Othello", 3560, new Type("tragedy")));

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
        var result = statementPrinter.Print(invoice, plays, ".txt");

        Approvals.Verify(result);
    }

    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void TestTextStatementExample()
    {
        var plays = new Dictionary<string, Play>();
        plays.Add("hamlet", new Play("Hamlet", 4024, new Type("tragedy")));
        plays.Add("as-like", new Play("As You Like It", 2670, new Type("comedy")));
        plays.Add("othello", new Play("Othello", 3560, new Type("tragedy")));
        plays.Add("henry-v", new Play("Henry V", 3227, new Type("history")));
        plays.Add("john", new Play("King John", 2648, new Type("history")));
        plays.Add("richard-iii", new Play("Richard III", 3718, new Type("history")));

        Invoice invoice = new Invoice(
            "BigCo",
            new List<Performance>
            {
                new Performance("hamlet", 55),
                new Performance("as-like", 35),
                new Performance("othello", 40),
                new Performance("henry-v", 20),
                new Performance("john", 39),
                new Performance("henry-v", 20)
            }
        );

        StatementPrinter statementPrinter = new StatementPrinter();
        var result = statementPrinter.Print(invoice, plays, ".txt");

        Approvals.Verify(result);
    }

    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void TestXmlStatementExample()
    {
        var plays = new Dictionary<string, Play>();
        plays.Add("hamlet", new Play("Hamlet", 4024, new Type("tragedy")));
        plays.Add("as-like", new Play("As You Like It", 2670, new Type("comedy")));
        plays.Add("othello", new Play("Othello", 3560, new Type("tragedy")));
        plays.Add("henry-v", new Play("Henry V", 3227, new Type("history")));
        plays.Add("john", new Play("King John", 2648, new Type("history")));
        plays.Add("richard-iii", new Play("Richard III", 3718, new Type("history")));

        Invoice invoice = new Invoice(
            "BigCo",
            new List<Performance>
            {
                new Performance("hamlet", 55),
                new Performance("as-like", 35),
                new Performance("othello", 40),
                new Performance("henry-v", 20),
                new Performance("john", 39),
                new Performance("henry-v", 20)
            }
        );

        StatementPrinter statementPrinter = new StatementPrinter();
        var result = statementPrinter.Print(invoice, plays, ".xml");

        Approvals.Verify(result);
    }

    //Unit test for CalcAmount method
    [Theory]
    [InlineData("tragedy", 55, 4024, 65240)]
    [InlineData("comedy", 35, 2670, 54700)]
    [InlineData("tragedy", 40, 3560, 45600)]
    [InlineData("history", 20, 3227, 70540)]
    [InlineData("history", 39, 2648, 93160)]
    public void TestCalcAmountMethod(string typeName, int audience, int lines, int expectedResult)
    {
        Type testType = new Type(typeName);
        Performance testPerf = new Performance("test", audience);
        var result = testType.CalcAmount(testPerf, lines);
        Assert.Equal(expectedResult, result);
    }

    //Unit test for CalcCredits method
    [Theory]
    [InlineData("tragedy", 55, 25)]
    [InlineData("comedy", 35, 12)]
    [InlineData("tragedy", 40, 10)]
    [InlineData("history", 20, 0)]
    [InlineData("history", 39, 9)]
    public void TestCalcCreditsMethod(string typeName, int audience, int expectedResult){
        Type testType = new Type(typeName);
        Performance testPerf = new Performance("test", audience);
        var result = testType.CalcCredits(testPerf);
        Assert.Equal(expectedResult, result);
    }
}
