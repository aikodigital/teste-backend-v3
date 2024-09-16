using System.Collections.Generic;
using ApprovalTests;
using ApprovalTests.Reporters;
using TheatricalPlayersRefactoringKata.Application.Services;
using TheatricalPlayersRefactoringKata.Entities;
using TheatricalPlayersRefactoringKata.Enum;
using TheatricalPlayersRefactoringKata.Infrastructure.Services;
using TheatricalPlayersRefactoringKata.Services.PlayAmount;
using TheatricalPlayersRefactoringKata.Services.PlayVolumeCredits;
using Xunit;

namespace TheatricalPlayersRefactoringKata.Tests.Services;

public class StatementServiceTests
{
    private readonly IStatementService _sut = new StatementService(
        playAmountService: new PlayAmountService(),
        playVolumeCreditsService: new PlayVolumeCreditsService()
    );

    private readonly Dictionary<string, PlayEntity> _playsFixture = new()
    {
        { "hamlet", new PlayEntity("Hamlet", 4024, PlayTypeEnum.Tragedy) },
        { "as-like", new PlayEntity("As You Like It", 2670, PlayTypeEnum.Comedy) },
        { "othello", new PlayEntity("Othello", 3560, PlayTypeEnum.Tragedy) },
        { "henry-v", new PlayEntity("Henry V", 3227, PlayTypeEnum.History) },
        { "john", new PlayEntity("King John", 2648, PlayTypeEnum.History) },
        { "richard-iii", new PlayEntity("Richard III", 3718, PlayTypeEnum.History) }
    };

    private readonly InvoiceEntity _invoiceFixture = new(
        "BigCo",
        new List<PerformanceEntity>
        {
            new("hamlet", 55),
            new("as-like", 35),
            new("othello", 40),
            new("henry-v", 20),
            new("john", 39),
            new("henry-v", 20)
        }
    );

    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void TestTextStatementExample()
    {
        var statement = _sut.Create(_invoiceFixture, _playsFixture);
        var result = _sut.PrintText(statement);
        Approvals.Verify(result);
    }

    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void TestXmlStatementExample()
    {
        var statement = _sut.Create(_invoiceFixture, _playsFixture);
        var result = _sut.PrintXml(statement);
        Approvals.Verify(result);
    }
}