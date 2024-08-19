using System;
using System.Collections.Generic;
using System.Linq;
using ApprovalTests;
using ApprovalTests.Reporters;
using TheatricalPlayersRefactoringKata.Application;
using TheatricalPlayersRefactoringKata.Application.Services;
using TheatricalPlayersRefactoringKata.Domain.Entities;
using TheatricalPlayersRefactoringKata.Domain.Enums;
using Xunit;

namespace TheatricalPlayersRefactoringKata.Tests;

public class StatementPrinterTests
{
    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void TestTextStatementExample()
    {
        var hamlet = new Play(Guid.NewGuid(), "Hamlet", 4024, Genre.Tragedy);
        var asYouLikeIt = new Play(Guid.NewGuid(), "As You Like It", 2670, Genre.Comedy);
        var othello = new Play(Guid.NewGuid(), "Othello", 3560, Genre.Tragedy);
        var henryV = new Play(Guid.NewGuid(), "Henry V", 3227, Genre.History);
        var kingJohn = new Play(Guid.NewGuid(), "King John", 2648, Genre.History);
        var richardIII = new Play(Guid.NewGuid(), "Richard III", 3718, Genre.History);

        var invoice = new Invoice(Guid.NewGuid(), "BigCo", new List<Performance>()
        {
            new Performance(hamlet.Id, hamlet, 55),
            new Performance(asYouLikeIt.Id, asYouLikeIt, 35),
            new Performance(othello.Id, othello, 40),
            new Performance(henryV.Id, henryV, 20),
            new Performance(kingJohn.Id, kingJohn, 39),
            new Performance(henryV.Id, henryV, 20)
        });

        var plays = new Dictionary<Guid, Play>
    {
        { hamlet.Id, hamlet },
        { asYouLikeIt.Id, asYouLikeIt },
        { othello.Id, othello },
        { henryV.Id, henryV },
        { kingJohn.Id, kingJohn }
    };

        var statementFormatter = new TextStatementFormatter();
        var statementPrinter = new StatementPrinter(statementFormatter);
        var result = statementPrinter.Print(invoice, plays);

        Approvals.Verify(result);
    }

    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void TestXmlStatementExample()
    {
        var hamlet = new Play(Guid.NewGuid(), "Hamlet", 4024, Genre.Tragedy);
        var asYouLikeIt = new Play(Guid.NewGuid(), "As You Like It", 2670, Genre.Comedy);
        var othello = new Play(Guid.NewGuid(), "Othello", 3560, Genre.Tragedy);
        var henryV = new Play(Guid.NewGuid(), "Henry V", 3227, Genre.History);
        var kingJohn = new Play(Guid.NewGuid(), "King John", 2648, Genre.History);
        var richardIII = new Play(Guid.NewGuid(), "Richard III", 3718, Genre.History);

        var invoice = new Invoice(Guid.NewGuid(), "BigCo", new List<Performance>()
        {
            new Performance(hamlet.Id, hamlet, 55),
            new Performance(asYouLikeIt.Id, asYouLikeIt, 35),
            new Performance(othello.Id, othello, 40),
            new Performance(henryV.Id, henryV, 20),
            new Performance(kingJohn.Id, kingJohn, 39),
            new Performance(henryV.Id, henryV, 20)
        });

        var plays = new Dictionary<Guid, Play>
    {
        { hamlet.Id, hamlet },
        { asYouLikeIt.Id, asYouLikeIt },
        { othello.Id, othello },
        { henryV.Id, henryV },
        { kingJohn.Id, kingJohn }
    };

        var statementFormatter = new XmlStatementFormatter();
        var statementPrinter = new StatementPrinter(statementFormatter);
        var result = statementPrinter.Print(invoice, plays);

        Approvals.Verify(result);
    }
}
