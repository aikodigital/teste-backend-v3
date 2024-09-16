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
        var plays = new Dictionary<string, Play>
        {
            { "hamlet", new("Hamlet", 4024, "tragedy") },
            { "as-like", new("As You Like It", 2670, "comedy") },
            { "othello", new("Othello", 3560, "tragedy") }
        };

        Invoice invoice = new Invoice(
            "BigCo",
            new List<Performance>
            {
                new("hamlet", 55),
                new("as-like", 35),
                new("othello", 40),
            }
        );

        StatementPrinter statementPrinter = new StatementPrinter();
        Converter converterJsonToTxt = new Converter();
        var printResult = statementPrinter.Print(invoice, plays);

        var result = converterJsonToTxt.ConvertJsonToTxt(printResult);

        Approvals.Verify(result);
    }

    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void TestTextStatementExample()
    {
        var plays = new Dictionary<string, Play>
        {
            { "hamlet", new Play("Hamlet", 4024, "tragedy") },
            { "as-like", new Play("As You Like It", 2670, "comedy") },
            { "othello", new Play("Othello", 3560, "tragedy") },
            { "henry-v", new Play("Henry V", 3227, "history") },
            { "john", new Play("King John", 2648, "history") },
            { "richard-iii", new Play("Richard III", 3718, "history") }
        };

        Invoice invoice = new Invoice(
            "BigCo",
            new List<Performance>
            {
                new("hamlet", 55),
                new("as-like", 35),
                new("othello", 40),
                new("henry-v", 20),
                new("john", 39),
                new("henry-v", 20)
            }
        );

        StatementPrinter statementPrinter = new StatementPrinter();
        Converter converterJsonToTxt = new Converter();
        var printResult = statementPrinter.Print(invoice, plays);

        var result = converterJsonToTxt.ConvertJsonToTxt(printResult);

        Approvals.Verify(result);
    }
    
    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void TestXmlStatementExample()
    {
        var plays = new Dictionary<string, Play>
        {
            { "hamlet", new Play("Hamlet", 4024, "tragedy") },
            { "as-like", new Play("As You Like It", 2670, "comedy") },
            { "othello", new Play("Othello", 3560, "tragedy") },
            { "henry-v", new Play("Henry V", 3227, "history") },
            { "john", new Play("King John", 2648, "history") },
            { "richard-iii", new Play("Richard III", 3718, "history") }
        };

        Invoice invoice = new Invoice(
            "BigCo",
            new List<Performance>
            {
                new("hamlet", 55),
                new("as-like", 35),
                new("othello", 40),
                new("henry-v", 20),
                new("john", 39),
                new("henry-v", 20)
            }
        );

        StatementPrinter statementPrinter = new StatementPrinter();
        Converter jsonToXmlConverter = new Converter();

        var jsonResult = statementPrinter.Print(invoice, plays);
        string result = jsonToXmlConverter.ConvertJsonToXml(jsonResult);

        Approvals.Verify(result);
    }
}
