using System.Collections.Generic;
using ApprovalTests;
using ApprovalTests.Reporters;
using Xunit;
using TheatricalPlayersRefactoringKata.Domain.Entity;
using TheatricalPlayersRefactoringKata.Application.StatementPrinter;
using TheatricalPlayersRefactoringKata.Application.Genres;

namespace TheatricalPlayersRefactoringKata.Tests;

public class StatementPrinterTests
{
    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void TestStatementExampleLegacy()
    {
        var plays = new Dictionary<string, Play>();
        plays.Add("hamlet", new Tragedy("Hamlet", 4024));
        plays.Add("as-like", new Comedy("As You Like It", 2670));
        plays.Add("othello", new Tragedy("Othello", 3560));

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
        var result = statementPrinter.TextPrint(invoice, plays);

        Approvals.Verify(result);
    }

    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void TestTextStatementExample()
    {
        var plays = new Dictionary<string, Play>();
        plays.Add("hamlet", new Tragedy("Hamlet", 4024));
        plays.Add("as-like", new Comedy("As You Like It", 2670));
        plays.Add("othello", new Tragedy("Othello", 3560));
        plays.Add("henry-v", new History("Henry V", 3227));
        plays.Add("john", new History("King John", 2648));
        plays.Add("richard-iii", new History("Richard III", 3718));

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
        var result = statementPrinter.TextPrint(invoice, plays);

        Approvals.Verify(result);
    }
    
    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void TestXmlStatementExample()
    {
        var plays = new Dictionary<string, Play>
        {
            { "hamlet", new Tragedy("Hamlet", 4024) },
            { "as-like", new Comedy("As You Like It", 2670) },
            { "othello", new Tragedy("Othello", 3560) },
            { "henry-v", new History("Henry V", 3227) },
            { "john", new History("King John", 2648) },
            { "richard-iii", new History("Richard III", 3718) }
        };

        var invoice = new Invoice(
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

        var xmlPrinter = new StatementPrinter();
        var result = xmlPrinter.XmlPrint(invoice, plays);

        Approvals.Verify(result);
    }
}
