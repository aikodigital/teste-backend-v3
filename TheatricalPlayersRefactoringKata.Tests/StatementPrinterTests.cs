using System;
using System.Collections.Generic;
using ApprovalTests;
using ApprovalTests.Reporters;
using Xunit;
using TheatricalPlayersRefactoringKata.Enums;
using TheatricalPlayersRefactoringKata.Models;
using TheatricalPlayersRefactoringKata.OutputStrategies;
using System.IO;

namespace TheatricalPlayersRefactoringKata.Tests;


public class StatementPrinterTests
{
    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void TestStatementExampleLegacy()
    {
        var plays = new Dictionary<string, Play>();
        plays.Add("hamlet", new Play("Hamlet", 4024, PlayType.Tragedy));
        plays.Add("as-like", new Play("As You Like It", 2670, PlayType.Comedy));
        plays.Add("othello", new Play("Othello", 3560, PlayType.Tragedy));

        Invoice invoice = new Invoice(
            "BigCo",
            new List<Performance>
            {
                new Performance("hamlet", 55),
                new Performance("as-like", 35),
                new Performance("othello", 40),
            }
        );

        IStatementOutputStrategy outputStrategy = new TxtStatementOutputStrategy();
        StatementPrinter statementPrinter = new StatementPrinter(outputStrategy);
        var result = statementPrinter.Print(invoice, plays);

        Approvals.Verify(result);
    }

    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void TestTextStatementExample()
    {
        var plays = new Dictionary<string, Play>();
        plays.Add("hamlet", new Play("Hamlet", 4024, PlayType.Tragedy));
        plays.Add("as-like", new Play("As You Like It", 2670, PlayType.Comedy));
        plays.Add("othello", new Play("Othello", 3560, PlayType.Tragedy));
        plays.Add("henry-v", new Play("Henry V", 3227, PlayType.History));
        plays.Add("john", new Play("King John", 2648, PlayType.History));
        plays.Add("richard-iii", new Play("Richard III", 3718, PlayType.History));

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

        IStatementOutputStrategy outputStrategy = new TxtStatementOutputStrategy();
        StatementPrinter statementPrinter = new StatementPrinter(outputStrategy);
        var result = statementPrinter.Print(invoice, plays);

        Approvals.Verify(result);
    }

    //Was necessary the use of a different Reporter, ClipboardReporter, to make a move on the order of archives, which caused the Test to complain about an error, but was some difference on the Encoding
    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void TestXmlStatementExample()
    {
        var plays = new Dictionary<string, Play>();
        plays.Add("hamlet", new Play("Hamlet", 4024, PlayType.Tragedy));
        plays.Add("as-like", new Play("As You Like It", 2670, PlayType.Comedy));
        plays.Add("othello", new Play("Othello", 3560, PlayType.Tragedy));
        plays.Add("henry-v", new Play("Henry V", 3227, PlayType.History));
        plays.Add("john", new Play("King John", 2648, PlayType.History));
        plays.Add("richard-iii", new Play("Richard III", 3718, PlayType.History));

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

        IStatementOutputStrategy outputStrategy = new XmlStatementOutputStrategy();
        StatementPrinter statementPrinter = new StatementPrinter(outputStrategy);
        var result = statementPrinter.Print(invoice, plays);
        Approvals.Verify(result);
    }
}
