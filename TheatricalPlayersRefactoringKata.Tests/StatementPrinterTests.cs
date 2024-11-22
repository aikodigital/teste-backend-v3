using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using System.Threading.Tasks;
using ApprovalTests;
using ApprovalTests.Reporters;
using Moq;
using TheatricalPlayersRefactoringKata.Application;
using TheatricalPlayersRefactoringKata.Application.Factories;
using TheatricalPlayersRefactoringKata.Application.Files;
using TheatricalPlayersRefactoringKata.Application.Formatters;
using TheatricalPlayersRefactoringKata.Application.Interfaces;
using TheatricalPlayersRefactoringKata.Application.Models;
using TheatricalPlayersRefactoringKata.Domain.Interfaces;
using TheatricalPlayersRefactoringKata.Infrastructure.Utilities;
using Xunit;

namespace TheatricalPlayersRefactoringKata.Tests;

public class StatementPrinterTests
{

    private readonly StatementPrinter _statementPrinter = new ();
    private readonly Dictionary<string, Play> _plays = MockData.GetPlays();
    private readonly Dictionary<string, Play> _plays1 = MockData.GetPlays1();
    private readonly Invoice _invoice = MockData.GetInvoice();
    private readonly Invoice _invoice1 = MockData.GetInvoice1();

    [Obsolete]
    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void TestStatementExampleLegacy()
    {

        var Plays = _plays;

        var Invoice = _invoice;

        var result = _statementPrinter.Print(Invoice, Plays, "text");

        Approvals.Verify(result);

    }

    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void TestTextStatementExample()
    {

        var Plays = _plays1;

        var Invoice = _invoice1;

        var result = _statementPrinter.Print(Invoice, Plays, "text");

        Approvals.Verify(result);
    }

    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void TestXmlStatementExample()
    {
        var Plays = _plays1;

        var Invoice = _invoice1;

        var result = _statementPrinter.Print(Invoice, Plays, "xml");

        Approvals.Verify(result);
    }

    [Fact]
    public void TestInvalidFormatThrowsException()
    {
        var Plays = _plays1;
        var Invoice = _invoice1;

        //Adding a format output that not exists yet...
        Assert.Throws<ArgumentException>(() => _statementPrinter.Print(Invoice, Plays, "json"));
    }

    [Fact]
    public void TestInvalidPlayGenreThrowsException()
    {
        //Adding a genre/type that not exists yet...

        var play = TestDataBuilder.CreatePlay("Alien", 3000, "sci-fi");
        var performance = TestDataBuilder.CreatePerformance("alien", play.Name, 30, play.Lines, play.Type);

        Assert.Throws<ArgumentException>(() => GenreStrategyFactory.Create(performance.Play.Type));
    }

    [Fact]
    public void CalculateCost_WithNegativeAudience_ThrowsException()
    {
        var play = TestDataBuilder.CreatePlay("Invalid Play", 3000, "comedy");

        Assert.Throws<ArgumentOutOfRangeException>(() =>
        {
            var performance = new Performance("invalid-play", -5) { Play = play };
        });
    }

    [Theory]
    [MemberData(nameof(TestDataBuilder.BaseValueBasedOnLinesTest), MemberType = typeof(TestDataBuilder))]
    public void Should_CalculateBaseValue_BasedOnLines(int lines, decimal expectedBaseValue)
    {
        //Adding a genre/type that not exists yet...

        var play = TestDataBuilder.CreatePlay("The Miserables", lines, "history");
        var performance = TestDataBuilder.CreatePerformance("miserables", play.Name, 30, play.Lines, play.Type);

        AssertPerformanceBasePrice(performance, expectedBaseValue);
    }

    [Theory]
    [MemberData(nameof(TestDataBuilder.TragedyCostBasedOnAudienceTest), MemberType = typeof(TestDataBuilder))]
    public void Should_CalculateTragedyValue_BasedOnAudience(int audience, decimal expectedValue)
    {

        var play = TestDataBuilder.CreatePlay("Hamlet", 3000, "tragedy");
        var performance = TestDataBuilder.CreatePerformance("hamlet", play.Name, audience, play.Lines, play.Type);

        AssertPerformanceCost(performance, expectedValue);

    }


    [Theory]
    [MemberData(nameof(TestDataBuilder.ComedyCostBasedOnAudienceTest), MemberType = typeof(TestDataBuilder))]
    public void Should_CalculateComedyValue_BasedOnAudience(int audience, decimal expectedValue)
    {

        var play = TestDataBuilder.CreatePlay("As You Like It", 3000, "comedy");
        var performance = TestDataBuilder.CreatePerformance("as-like", play.Name, audience, play.Lines, play.Type);

        AssertPerformanceCost(performance, expectedValue);
    }

    [Theory]
    [MemberData(nameof(TestDataBuilder.CreditsBasedOnAudienceTest), MemberType = typeof(TestDataBuilder))]
    public void Should_CalculateCredits_BasedOnAudience(string genre, int audience, int expectedCredits)
    {
        
        var play = TestDataBuilder.CreatePlay("Test Play", 3000, genre);
        var performance = TestDataBuilder.CreatePerformance("test-play", play.Name, audience, play.Lines, play.Type);

        AssertPerformanceCredits(performance, expectedCredits);
    }

    [Theory]
    [MemberData(nameof(TestDataBuilder.HistoricalPlayTest), MemberType = typeof(TestDataBuilder))]
    public void Should_CalculateHistoricalPlayValue(int audience, decimal expectedValue)
    {

        var play = TestDataBuilder.CreatePlay("King Arthur", 3000, "history");
        var performance = TestDataBuilder.CreatePerformance("king-arthur", play.Name, audience, play.Lines, play.Type);

        AssertPerformanceCost(performance, expectedValue);
    }

    private void AssertPerformanceCredits(Performance performance, decimal expectedCredits)
    {
        var strategy = GenreStrategyFactory.Create(performance.Play.Type);
        CalculatePerformanceCostAndCredits(performance, strategy);

        Assert.Equal(expectedCredits, performance.Credits);
    }

    private void AssertPerformanceBasePrice(Performance performance, decimal expectedCost)
    {
        var strategy = GenreStrategyFactory.Create(performance.Play.Type);
        CalculatePerformanceCostAndCredits(performance, strategy);

        Assert.Equal(expectedCost, performance.BasePrice);
    }
    private void AssertPerformanceCost(Performance performance, decimal expectedCost)
    {
        var strategy = GenreStrategyFactory.Create(performance.Play.Type);
        CalculatePerformanceCostAndCredits(performance, strategy);

        Assert.Equal(expectedCost, performance.Cost);
    }
    private void CalculatePerformanceCostAndCredits(Performance performance, IGenreStrategy strategyGenreFactory)
    {


        performance.Cost = strategyGenreFactory.CalculateCost(performance.Audience, performance.Play.Lines);
        performance.Credits = strategyGenreFactory.CalculateCredits(performance.Audience);
        performance.BasePrice = strategyGenreFactory.CalculateBasePrice(performance.Play.Lines);
    }

}
