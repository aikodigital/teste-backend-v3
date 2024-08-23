using System;
using System.Collections.Generic;
using ApprovalTests;
using ApprovalTests.Reporters;
using TheatricalPlayersRefactoringKata.Application.Enums;
using TheatricalPlayersRefactoringKata.Domain.Core.Interfaces.IRepositories;
using TheatricalPlayersRefactoringKata.Domain.Core.Services;
using TheatricalPlayersRefactoringKata.Domain.Core.Strategy;
using TheatricalPlayersRefactoringKata.Infrastructure.Repositories;
using Xunit;

namespace TheatricalPlayersRefactoringKata.Tests;

public class StatementPrinterTests {

    // obsoleto
    //[Fact]
    //[UseReporter(typeof(DiffReporter))]
    //public void TestStatementExampleLegacy() {
    //    Dictionary<Enum, IGenreStrategy> genres = new Dictionary<Enum, IGenreStrategy> {
    //        { PlayGenre.Comedy, new ComedyGenreStrategy() },
    //        { PlayGenre.Tragedy, new TragedyGenreStrategy() },
    //        { PlayGenre.History, new HistoryGenreStrategy() },
    //    };
    //    StatementPrinterRepositoryImpl repository = new(genres);


    //    var plays = new Dictionary<string, Play>();
    //    plays.Add("hamlet", new("Hamlet", 4024, PlayGenre.Tragedy));
    //    plays.Add("as-like", new("As You Like It", 2670, PlayGenre.Comedy));
    //    plays.Add("othello", new("Othello", 3560, PlayGenre.Tragedy));

    //    Invoice invoice = new Invoice(
    //        "BigCo",
    //        new List<Performance>
    //        {
    //            new("hamlet", 55),
    //            new("as-like", 35),
    //            new("othello", 40),
    //        }
    //    );

    //    StatementPrinterService statementPrinter = new(repository);
    //    var result = statementPrinter.Print(invoice, plays);

    //    Approvals.Verify(result.Value);
    //}

    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void TestTextStatementExample() {
        Dictionary<Enum, IGenreStrategy> genres = InitializeGenres();
        Dictionary<string, Play> plays = InitializePlays();
        Invoice invoice = CreateInvoice();

        StatementPrinterRepositoryImpl repository = new();
        StatementPrinterService statementPrinter = new(repository);
        var result = statementPrinter.PrintText(invoice, plays, genres);


        Approvals.Verify(result.Value);
    }

    private Dictionary<Enum, IGenreStrategy> InitializeGenres() {
        return new Dictionary<Enum, IGenreStrategy> {
        { PlayGenre.Comedy, new ComedyGenreStrategy() },
        { PlayGenre.Tragedy, new TragedyGenreStrategy() },
        { PlayGenre.History, new HistoryGenreStrategy() },
    };
    }

    private Dictionary<string, Play> InitializePlays() {
        return new Dictionary<string, Play>
        {
        { "hamlet", new("Hamlet", 4024, PlayGenre.Tragedy) },
        { "as-like", new("As You Like It", 2670, PlayGenre.Comedy) },
        { "othello", new("Othello", 3560, PlayGenre.Tragedy) },
        { "henry-v", new("Henry V", 3227, PlayGenre.History) },
        { "john", new("King John", 2648, PlayGenre.History) },
        { "richard-iii", new("Richard III", 3718, PlayGenre.History) }
    };
    }

    private Invoice CreateInvoice() {
        return new Invoice(
            "BigCo",
            new List<Performance>
            {
            new("hamlet", 55),
            new("as-like", 35),
            new("othello", 40),
            new("henry-v", 20),
            new("john", 39),
            new("henry-v", 20)
            }
        );
    }
}
