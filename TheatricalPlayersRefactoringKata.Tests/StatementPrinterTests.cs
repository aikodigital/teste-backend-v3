using System;
using System.Collections.Generic;
using ApprovalTests;
using ApprovalTests.Reporters;
using TheatricalPlayersRefactoringKata.Application.Enums;
using TheatricalPlayersRefactoringKata.Domain.Core.Interfaces.IRepositories;
using TheatricalPlayersRefactoringKata.Domain.Core.Services;
using Xunit;

namespace TheatricalPlayersRefactoringKata.Tests;

public class StatementPrinterTests
{
    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void TestStatementExampleLegacy()
    {
        var plays = new Dictionary<string, Play>();
        plays.Add("hamlet", new Play("Hamlet", 4024, PlayGenre.Tragedy));
        plays.Add("as-like", new Play("As You Like It", 2670, PlayGenre.Comedy));
        plays.Add("othello", new Play("Othello", 3560, PlayGenre.Tragedy));

        Invoice invoice = new Invoice(
            "BigCo",
            new List<Performance>
            {
                new Performance("hamlet", 55),
                new Performance("as-like", 35),
                new Performance("othello", 40),
            }
        );

        StatementPrinterService statementPrinter = new(StatementPrinterRepositoryImpl);
        var result = statementPrinter.Print(invoice, plays);

        Approvals.Verify(result);
    }

    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void TestTextStatementExample()
    {
        var plays = new Dictionary<string, Play>();
        plays.Add("hamlet", new Play("Hamlet", 4024, PlayGenre.Tragedy));
        plays.Add("as-like", new Play("As You Like It", 2670, PlayGenre.Comedy));
        plays.Add("othello", new Play("Othello", 3560, PlayGenre.Tragedy));
        plays.Add("henry-v", new Play("Henry V", 3227, PlayGenre.History));
        plays.Add("john", new Play("King John", 2648, PlayGenre.History));
        plays.Add("richard-iii", new Play("Richard III", 3718, PlayGenre.History));

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

        StatementPrinterService statementPrinter = new (_repository);
        var result = statementPrinter.Print(invoice, plays);

        Approvals.Verify(result);
    }
}
