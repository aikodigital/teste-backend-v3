using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Xml.Linq;
using ApprovalTests;
using ApprovalTests.Reporters;
using TheatricalPlayersRefactoringKata.Models;
using TheatricalPlayersRefactoringKata.Services;
using Xunit;

namespace TheatricalPlayersRefactoringKata.Tests;

public class StatementPrinterTests
{
    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void TestStatementExampleLegacy()
    {
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
        var result = statementPrinter.Print(invoice, plays);

        Approvals.Verify(result);
    }

    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void TestTextStatementExample()
    {
        var plays = new Dictionary<string, Play>();
        plays.Add("hamlet", new Play("Hamlet", 4024, "tragedy"));
        plays.Add("as-like", new Play("As You Like It", 2670, "comedy"));
        plays.Add("othello", new Play("Othello", 3560, "tragedy"));
        plays.Add("henry-v", new Play("Henry V", 3227, "history"));
        plays.Add("john", new Play("King John", 2648, "history"));
        plays.Add("richard-iii", new Play("Richard III", 3718, "history"));

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
        var result = statementPrinter.Print(invoice, plays);

        Approvals.Verify(result);
    }

    //Test fail because the first line "﻿<?xml version="1.0" encoding="utf-8"?>
    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void TestXmlStatementExample()
    {
        var plays = new Dictionary<string, Play>();
        plays.Add("hamlet", new Play("Hamlet", 4024, "tragedy"));
        plays.Add("as-like", new Play("As You Like It", 2670, "comedy"));
        plays.Add("othello", new Play("Othello", 3560, "tragedy"));
        plays.Add("henry-v", new Play("Henry V", 3227, "history"));
        plays.Add("john", new Play("King John", 2648, "history"));
        plays.Add("richard-iii", new Play("Richard III", 3718, "history"));

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
        var result = statementPrinter.PrintXML(invoice, plays);

        Approvals.Verify(result);
    }
    [Theory]
    [InlineData("comedy", 40, 18)]
    [InlineData("tragedy", 30, 0)]
    [InlineData("history", 50, 20)]
    public void Test_PrintXML_EarnedCredits(string playType, int audience, int expectedCredits)
    {
        var invoice = new Invoice(
            "BigCo",
            new List<Performance>
            {
                new Performance(playType, audience)
            }
        );
        var plays = new Dictionary<string, Play>
        {
            [playType] = new Play(playType, 3000, playType)
        };

        var statementPrinter = new StatementPrinter();
        var xmlString = statementPrinter.PrintXML(invoice, plays);
        XDocument result = XDocument.Parse(xmlString);
        var earnedCreditsElement = result.Descendants("EarnedCredits").FirstOrDefault();
        Assert.NotNull(earnedCreditsElement);
        Assert.Equal(expectedCredits.ToString(), earnedCreditsElement.Value);
    }

    [Theory]
    [InlineData("comedy", 40,62000)]
    [InlineData("tragedy", 30, 30000)]
    [InlineData("history", 50, 120000)]
    public void Test_PrintXML_AmountOwed(string playType, int audience, double expectedAmount)
    {
        var invoice = new Invoice(
            "BigCo",
            new List<Performance>
            {
                new Performance(playType, audience)
            }
        );
        var plays = new Dictionary<string, Play>
        {
            [playType] = new Play(playType, 3000, playType)
        };

        var statementPrinter = new StatementPrinter();
        var xmlString = statementPrinter.PrintXML(invoice, plays);
        XDocument result = XDocument.Parse(xmlString);

        var amountOwedElement = result.Descendants("AmountOwed").FirstOrDefault();
        Assert.NotNull(amountOwedElement);
        Assert.Equal(string.Format("{0}", expectedAmount / 100), amountOwedElement.Value);
    }
}
