using Xunit;
using ApprovalTests;
using ApprovalTests.Reporters;
using System.Collections.Generic;
using TheatricalPlayersRefactoringKata;

public class StatementPrinterXmlTests
{
    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void TestXmlStatementWithHistoryPlay()
    {
        var plays = new Dictionary<string, Play>
        {
            { "henry-v", new Play("Henry V", 3227, "history") },
            { "john", new Play("King John", 2648, "history") },
            { "richard-iii", new Play("Richard III", 3718, "history") }
        };

        var invoice = new Invoice("BigCo", new List<Performance>
        {
            new Performance("henry-v", 20),
            new Performance("john", 39)
        });

        var statementPrinter = new StatementPrinter();

        var result = statementPrinter.PrintXml(invoice, plays);

        Approvals.Verify(result);
    }
}
