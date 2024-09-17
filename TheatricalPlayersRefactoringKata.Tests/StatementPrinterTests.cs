using Xunit;
using System.Collections.Generic;
using ApprovalTests;
using ApprovalTests.Reporters;
using TheatricalPlayersRefactoringKata.Legacy;
using TheatricalPlayersRefactoringKata.Domain.Entities;
using TheatricalPlayersRefactoringKata.Domain.Entities.Gender;
using TheatricalPlayersRefactoringKata.Application.Services.Statement;
using TheatricalPlayersRefactoringKata.Application.UserCases.Statement;

namespace TheatricalPlayersRefactoringKata.Tests;

public class StatementPrinterTests
{
    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void TestStatementExampleLegacy()
    {
        var plays = new Dictionary<string, Play>
        {
            { "hamlet", new Play("Hamlet", 4024, "tragedy") },
            { "as-like", new Play("As You Like It", 2670, "comedy") },
            { "othello", new Play("Othello", 3560, "tragedy") }
        };

        Legacy.Invoice invoice = new(
            "BigCo",
            new List<Legacy.Performance>
            {
                new("hamlet", 55),
                new ("as-like", 35),
                new ("othello", 40),
            }
        );

        var statementPrinter = new StatementPrinter();
        var result = statementPrinter.Print(invoice, plays);

        Approvals.Verify(result);
    }

    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void TestTextStatementExample()
    {
        var plays = new Dictionary<string, Domain.Entities.Performance>
        {
            { "hamlet", new Domain.Entities.Performance("Hamlet", 4024, 55, new Tragedy()) },
            { "as-like", new Domain.Entities.Performance("As You Like It", 2670, 35, new Comedy()) },
            { "othello", new Domain.Entities.Performance("Othello", 3560, 40, new Tragedy()) },
            { "henry-v", new Domain.Entities.Performance("Henry V", 3227, 20, new History(new Tragedy(), new Comedy())) },
            { "john", new Domain.Entities.Performance("King John", 2648, 39, new History(new Tragedy(), new Comedy())) },
            { "richard-iii", new Domain.Entities.Performance("Richard III", 3718, 20, new History(new Tragedy(), new Comedy())) }
        };

        Domain.Entities.Invoice invoice = new(
            new Customer("BigCo"),
            plays
        );

        var statement = new GenerateStatement(new StatementText());
        var result = statement.Execute(invoice);

        Approvals.Verify(result);
    }

    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void TestXmlStatementExample()
    {
        var plays = new Dictionary<string, Domain.Entities.Performance>
        {
            { "hamlet", new Domain.Entities.Performance("Hamlet", 4024, 55, new Tragedy()) },
            { "as-like", new Domain.Entities.Performance("As You Like It", 2670, 35, new Comedy()) },
            { "othello", new Domain.Entities.Performance("Othello", 3560, 40, new Tragedy()) },
            { "henry-v", new Domain.Entities.Performance("Henry V", 3227, 20, new History(new Tragedy(), new Comedy())) },
            { "john", new Domain.Entities.Performance("King John", 2648, 39, new History(new Tragedy(), new Comedy())) },
            { "richard-iii", new Domain.Entities.Performance("Richard III", 3718, 20, new History(new Tragedy(), new Comedy())) }
        };

        Domain.Entities.Invoice invoice = new(
            new Customer("BigCo"),
            plays
        );

        var statement = new GenerateStatement(new StatementXml());
        var result = statement.Execute(invoice);

        Approvals.Verify(result);
    }
}
