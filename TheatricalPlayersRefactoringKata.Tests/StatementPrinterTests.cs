using ApprovalTests;
using ApprovalTests.Reporters;
using Xunit;

using TheatricalPlayersRefactoringKata.Modules;
using TheatricalPlayersRefactoringKata.Services;
using System.Diagnostics;

namespace TheatricalPlayersRefactoringKata.Tests;

public class StatementPrinterTests
{
    private readonly Dictionary<string, Play> PLAYS = new Dictionary<string, Play>
        {
            { "Hamlet", new Play("Hamlet", 4024, new Tragedy()) },
            { "As You Like It", new Play("As You Like It", 2670, new Comedy()) },
            { "Othello", new Play("Othello", 3560, new Tragedy()) },
            { "Henry V", new Play("Henry V", 3227, new History()) },
            { "King John", new Play("King John", 2648, new History()) },
            { "Richard III", new Play("Richard III", 3718, new History()) },
        };

    private readonly Invoice INVOICE = new Invoice(
        "BigCo",
        new List<Performance>
        {
            new Performance("Hamlet", 55),
            new Performance("As You Like It", 35),
            new Performance("Othello", 40),
            new Performance("Henry V", 20),
            new Performance("King John", 39),
            new Performance("Henry V", 20)
        }
    );

    private readonly HttpClient _client;

    public StatementPrinterTests()
    {
        _client = new HttpClient
        {
            BaseAddress = new Uri("https://localhost:3000")
        };
    }

    private void TestStatement(OutputWritter outputWritter)
    {
        string statementResult = new StatementPrinter().Print(INVOICE, PLAYS, outputWritter, Path.Combine(Directory.GetCurrentDirectory(), $"StatementPrinterTestOutput.{outputWritter.FileType}"));
        Approvals.Verify(statementResult);
    }

    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void TestTextStatementExample() { TestStatement(new TextOutputWritter()); }

    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void TestXmlStatementExample() { TestStatement(new XMLOutputWritter()); }
}