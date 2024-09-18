using System;
using System.Collections.Generic;
using ApprovalTests;
using ApprovalTests.Reporters;
using Xunit;
using TheatricalPlayersRefactoringKata.Domain.Entities;
using TheatricalPlayersRefactoringKata.Domain.enums;
namespace TheatricalPlayersRefactoringKata.Tests;

public class StatementPrinterTests
{
    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void TestStatementExampleLegacy()
    {
        var plays = new Dictionary<string, Play>();
        plays.Add("hamlet", new Play("Hamlet", 4024, PlayType.tragedy));
        plays.Add("as-like", new Play("As You Like It", 2670, PlayType.comedy));
        plays.Add("othello", new Play("Othello", 3560, PlayType.tragedy));

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
        var result = StatementPrinter.Print(invoice, plays);

        Approvals.Verify(result);
    }

    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void TestTextStatementExample()
    {
        var plays = new Dictionary<string, Play>();
        plays.Add("hamlet", new Play("Hamlet", 4024, PlayType.tragedy));
        plays.Add("as-like", new Play("As You Like It", 2670, PlayType.comedy));
        plays.Add("othello", new Play("Othello", 3560, PlayType.tragedy));
        plays.Add("henry-v", new Play("Henry V", 3227, PlayType.history));
        plays.Add("john", new Play("King John", 2648, PlayType.history));
        plays.Add("richard-iii", new Play("Richard III", 3718, PlayType.history));

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
        var result = StatementPrinter.Print(invoice, plays);

        Approvals.Verify(result);
    }

    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void TestXmlStatementExample()
    {
        var plays = new Dictionary<string, Play>();
        plays.Add("hamlet", new Play("Hamlet", 4024, PlayType.tragedy));
        plays.Add("as-like", new Play("As You Like It", 2670, PlayType.comedy));
        plays.Add("othello", new Play("Othello", 3560, PlayType.tragedy));
        plays.Add("henry-v", new Play("Henry V", 3227, PlayType.history));
        plays.Add("john", new Play("King John", 2648, PlayType.history));
        plays.Add("richard-iii", new Play("Richard III", 3718, PlayType.history));

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

        var statement = new StatementGenerator(new StatementInput(invoice,plays)).GenerateStatement();
        string result =  StatementPrinter.PrintStatement(statement);
        Approvals.Verify(result);
    }
}
