using System.Collections.Generic;
using ApprovalTests;
using ApprovalTests.Reporters;
using TheatricalPlayersRefactoringKata.Core.Entities;
using TheatricalPlayersRefactoringKata.Core.Presentation.Formatters;
using TheatricalPlayersRefactoringKata.Core.Services;
using Xunit;

[Fact]
[UseReporter(typeof(DiffReporter))]
void TestTextStatementExample()
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

    var statementService = new StatementService();
    var formatter = new TextStatementFormatter(statementService);
    var result = formatter.Format(invoice, plays);

    Approvals.Verify(result);
}
