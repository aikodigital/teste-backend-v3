using System.Collections.Generic;
using ApprovalTests;
using ApprovalTests.Reporters;
using TheatricalPlayersRefactoringKata.Model;
using TheatricalPlayersRefactoringKata.Service.Printer;
using Xunit;

namespace TheatricalPlayersRefactoringKata.Tests;

public class StatementPrinterTests
{
    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void TestStatementExampleLegacy()
    {
        var plays = new Dictionary<string, Play>()
        {
            {"hamlet", new Play("Hamlet", 4024, Genre.tragedy)},
            {"as-like", new Play("As You Like It", 2670, Genre.comedy)},
            {"othello", new Play("Othello", 3560, Genre.tragedy)},
        };

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
        var plays = new Dictionary<string, Play>()
        {
            {"hamlet", new Play("Hamlet", 4024, Genre.tragedy)},
            {"as-like", new Play("As You Like It", 2670, Genre.comedy)},
            {"othello", new Play("Othello", 3560, Genre.tragedy)},
            {"henry-v", new Play("Henry V", 3227, Genre.history)},
            {"john", new Play("King John", 2648, Genre.history)},
            {"richard-iii", new Play("Richard III", 3718, Genre.history)},
        };

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
    
    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void TestXmlStatementExample()
    {
       var plays = new Dictionary<string, Play>()
        {
            {"hamlet", new Play("Hamlet", 4024, Genre.tragedy)},
            {"as-like", new Play("As You Like It", 2670, Genre.comedy)},
            {"othello", new Play("Othello", 3560, Genre.tragedy)},
            {"henry-v", new Play("Henry V", 3227, Genre.history)},
            {"john", new Play("King John", 2648, Genre.history)},
            {"richard-iii", new Play("Richard III", 3718, Genre.history)},
        };

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

        XmlStatementPrinter xmlStatementPrinter = new XmlStatementPrinter();
        var result = xmlStatementPrinter.Print(invoice, plays);
        
        Approvals.Verify(result);
    }
}
