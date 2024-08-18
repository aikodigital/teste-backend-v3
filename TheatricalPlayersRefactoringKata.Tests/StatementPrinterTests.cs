using ApprovalTests;
using ApprovalTests.Reporters;
using Xunit;

using TheatricalPlayersRefactoringKata.Modules;
using TheatricalPlayersRefactoringKata.Services;
using Xunit.Abstractions;

namespace TheatricalPlayersRefactoringKata.Tests;

public class StatementPrinterTests
{
    private readonly Dictionary<string, Play> plays = new Dictionary<string, Play>
        {
            { "hamlet", new Play("Hamlet", 4024, new Tragedy()) },
            { "as-like", new Play("As You Like It", 2670, new Comedy()) },
            { "othello", new Play("Othello", 3560, new Tragedy()) },
            { "henry-v", new Play("Henry V", 3227, new History()) },
            { "john", new Play("King John", 2648, new History()) },
            { "richard-iii", new Play("Richard III", 3718, new History()) },
        };

    private readonly Invoice invoice = new Invoice(
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

    private void TestStatement(OutputWritter outputWritter)
    {
        string statementResult = new StatementPrinter().Print(invoice, plays, outputWritter, Path.Combine(Directory.GetCurrentDirectory(), $"StatementPrinterTestOutput.{outputWritter.FileType}"));
        Approvals.Verify(statementResult);
    }

    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void TestTextStatementExample() { TestStatement(new TextOutputWritter()); }

    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void TestXmlStatementExample() { TestStatement(new XMLOutputWritter()); }
}