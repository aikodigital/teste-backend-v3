using System.Collections.Generic;
using ApprovalTests;
using ApprovalTests.Reporters;
using Xunit;
using TheatricalPlayersRefactoringKata.Domain.Entity;
using TheatricalPlayersRefactoringKata.Application.StatementPrinter;
using TheatricalPlayersRefactoringKata.Application.Genres;
using TheatricalPlayersRefactoringKata.Domain.Enum;

namespace TheatricalPlayersRefactoringKata.Tests;

public class StatementPrinterTests
{
    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void TestStatementExampleLegacy()
    {
        var plays = new Dictionary<string, Play>();
        plays.Add("hamlet", new Tragedy("Hamlet", 4024, EnumGenres.Tragedy));
        plays.Add("as-like", new Comedy("As You Like It", 2670, EnumGenres.Comedy));
        plays.Add("othello", new Tragedy("Othello", 3560, EnumGenres.Tragedy));

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
        plays.Add("hamlet", new Tragedy("Hamlet", 4024, EnumGenres.Tragedy));
        plays.Add("as-like", new Comedy("As You Like It", 2670, EnumGenres.Comedy));
        plays.Add("othello", new Tragedy("Othello", 3560, EnumGenres.Tragedy));
        plays.Add("henry-v", new History("Henry V", 3227, EnumGenres.History));
        plays.Add("john", new History("King John", 2648, EnumGenres.History));
        plays.Add("richard-iii", new History("Richard III", 3718, EnumGenres.History));

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
}
