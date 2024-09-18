using System;
using System.Collections.Generic;
using System.Linq;
using ApprovalTests;
using ApprovalTests.Reporters;
using Xunit;

namespace TheatricalPlayersRefactoringKata.Tests;

public class StatementPrinterTests
{
    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void TestCalculateAmountTragedy()
    {
        Approvals.Verify(
            String.Format(
                "Example 1: {0}, Example 2: {1}",
                StatementPrinter.CalculateAmount("tragedy", 5000, 25),
                StatementPrinter.CalculateAmount("tragedy", 5000, 35)
            )
        );
    }

    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void TestCalculateAmountComedy()
    {
        Approvals.Verify(
            String.Format(
                "Example 1: {0}, Example 2: {1}",
                StatementPrinter.CalculateAmount("comedy", 5000, 15),
                StatementPrinter.CalculateAmount("comedy", 5000, 25)
            )
        );
    }

    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void TestCalculateAmountHistory()
    {
        Approvals.Verify(
             String.Format(
                 "Example 1: {0}, Example 2: {1}, Example 3: {2}",
                 StatementPrinter.CalculateAmount("history", 5000, 15),
                 StatementPrinter.CalculateAmount("history", 5000, 25),
                 StatementPrinter.CalculateAmount("history", 5000, 35)
             )
         );
    }

    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void TestCalculateCreditsTragedy()
    {
        Approvals.Verify(
            String.Format(
                "Example 1: {0}",
                StatementPrinter.CalculateCredits("tragedy", 5000, 25)
            )
        );
    }

    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void TestCalculateCreditsComedy()
    {
        Approvals.Verify(
            String.Format(
                "Example 1: {0}, Example 2: {1}, Example 3: {2}",
                StatementPrinter.CalculateCredits("comedy", 5000, 15),
                StatementPrinter.CalculateCredits("comedy", 5000, 25),
                StatementPrinter.CalculateCredits("comedy", 5000, 35)
            )
        );
    }

    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void TestCalculateCreditsHistory()
    {
        Approvals.Verify(
             String.Format(
                 "Example 1: {0}",
                 StatementPrinter.CalculateCredits("history", 5000, 15)
             )
         );
    }

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
}
