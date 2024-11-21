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

    [Obsolete]
    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void TestStatementExampleLegacy()
    {

        var Plays = MockData.GetPlays();

        var Invoice = MockData.GetInvoice();

        Application.StatementPrinter statementPrinter = new Application.StatementPrinter();
        var result = statementPrinter.Print(Invoice, Plays, "text");

        Approvals.Verify(result);

    }

    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void TestTextStatementExample()
    {

        var Plays = MockData.GetPlays1();

        var Invoice = MockData.GetInvoice1();

        Application.StatementPrinter statementPrinter = new Application.StatementPrinter();
        var result = statementPrinter.Print(Invoice, Plays, "text");

        Approvals.Verify(result);
    }

    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void TestXmlStatementExample()
    {
        var Plays = MockData.GetPlays1();

        var Invoice = MockData.GetInvoice1();

        Application.StatementPrinter statementPrinter = new Application.StatementPrinter();
        var result = statementPrinter.Print(Invoice, Plays, "xml");

        Approvals.Verify(result);
    }

    [Theory]
    [InlineData(500, 100.0)] // Limite inferior ajustado para 1000
    [InlineData(3500, 350.0)] // Dentro do intervalo
    [InlineData(4500, 400.0)] // Limite superior ajustado para 4000
    public void Should_CalculateBaseValue_BasedOnLines(int lines, decimal expectedBaseValue)
    {
        var play = new Play("Test Play", lines, "comedy");

        var baseValue = PricingHelper.CalculateBasePrice(play.Lines);


        Assert.Equal(expectedBaseValue, baseValue);
    }

    [Theory]
    [InlineData(30, 300.0)] // Base value only
    [InlineData(35, 350.0)] // Base + (5 * 10.0)
    public void Should_CalculateTragedyValue_BasedOnAudience(int audience, decimal expectedValue)
    {
        var play = new Play("Hamlet", 3000, "tragedy");
        var performance = new Performance("hamlet", audience);

        performance.Play = play;

        var strategyGenreFactory = GenreStrategyFactory.Create(performance.Play.Type);

        CalculatePerformanceCostAndCredits(performance, strategyGenreFactory);

        Assert.Equal(expectedValue, performance.Cost);
    }


    [Theory]
    [InlineData(15, 345.0)] // Base + (15 * 3.00)
    [InlineData(25, 500.0)] // Base + (25 * 3.00) + 100.00 + (5 * 5.00)
    public void Should_CalculateComedyValue_BasedOnAudience(int audience, decimal expectedValue)
    {
        var play = new Play("As You Like It", 3000, "comedy");
        var performance = new Performance("as-like", audience);
        performance.Play = play;

        var strategyGenreFactory = GenreStrategyFactory.Create(performance.Play.Type);

        CalculatePerformanceCostAndCredits(performance, strategyGenreFactory);

        Assert.Equal(expectedValue, performance.Cost);
    }

    [Theory]
    [InlineData("tragedy", 30, 0)] // No credits for audience <= 30
    [InlineData("tragedy", 40, 10)] // 10 audience over 30
    [InlineData("comedy", 30, 6)] // Comedy, audience = 30, 6 credit bonus over audience (1/5)
    [InlineData("comedy", 40, 18)] // 8 credits (1/5)
    [InlineData("history", 30, 0)] // No credits for audience <= 30
    [InlineData("history", 40, 10)] // 10 audience over 30
    public void Should_CalculateCredits_BasedOnAudience(string genre, int audience, int expectedCredits)
    {
        var play = new Play("Test Play", 3000, genre);
        var performance = new Performance("test-play", audience);

        performance.Play = play;

        var strategyGenreFactory = GenreStrategyFactory.Create(performance.Play.Type);

        CalculatePerformanceCostAndCredits(performance, strategyGenreFactory);

        Assert.Equal(expectedCredits, performance.Credits);
    }

    [Theory]
    [InlineData(25, 800.0)] // Tragedy: 300 + (0 * 10) = 300 Comedy: 300 + (25 * 3) + 100 + (5 * 5) = 500
    [InlineData(50, 1200.0)] // Tragedy: 300 + (20 * 10) = 500 Comedy: 300 + (50 * 3) + 100 + (30 * 5) = 700
    [InlineData(100, 2100.0)] // Tragedy: 300 + (70 * 10) = 1000 Comedy: 300 + (100 * 3) + 100 + (80 * 5) = 1100
    public void Should_CalculateHistoricalPlayValue(int audience, decimal expectedValue)
    {
        var play = new Play("Historical Play", 3000, "history");
        var performance = new Performance("historical-play", audience);

        performance.Play = play;

        var strategyGenreFactory = GenreStrategyFactory.Create(performance.Play.Type);

        CalculatePerformanceCostAndCredits(performance, strategyGenreFactory);
        
        Assert.Equal(expectedValue, performance.Cost);
    }


    //[Fact]
    //public async Task GenerateAndSaveStatementAsync_ShouldSaveStatementToFile()
    //{


    //    var formatterMock = new Mock<IStatementFormatter>();
    //    formatterMock.Setup(f => f.Format(It.IsAny<Invoice>()))
    //                 .Returns("<xml><statement>Formatted XML Statement</statement></xml>");

    //    var directory = @"C:\estudo\arquivos";
    //    var fileName = "TheatricalPlayers.xml";

    //    var fileServiceMock = new Mock<StatementFileService>();
    //    fileServiceMock.Setup(f => f.GenerateStatementFile(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
    //                   .Returns(Task.CompletedTask);

    //    var statementPrinterMock = new Mock<Application.StatementPrinter>();
    //    statementPrinterMock.Setup(sp => sp.Print(It.IsAny<Invoice>(), It.IsAny<Dictionary<string, Play>>(), It.IsAny<string>()))
    //                        .Returns("<xml><statement>Formatted XML Statement</statement></xml>");

    //    var result = statementPrinterMock.Object.Print(Invoice, Plays, "xml");
    //    await fileServiceMock.Object.GenerateStatementFile(result, directory, fileName, "xml");

    //    var savedContent = await File.ReadAllTextAsync($@"{directory}\{fileName}");
    //    Assert.Equal("<xml>Sample XML content</xml>", savedContent);
    //}

    //[Fact]
    //        public void Should_GenerateApprovedXmlStatement()
    //        {

    //            var formatter = StatementFormatterFactory.Create("xml");

    //            var statementPrinterMock = new Mock<Application.StatementPrinter>();

    //            statementPrinterMock.Setup(sp => sp.Print(It.IsAny<Invoice>(), It.IsAny<Dictionary<string, Play>>(), It.IsAny<string>()))
    //                            .Returns("Formatted Text Statement");

    //            var result = statementPrinterMock.Object.Print(Invoice, Plays, "xml");

    //            // Compare result with an approved XML
    //            Assert.Equal(@"
    //<statement>
    //  <customer>BigCo</customer>
    //  <performance>
    //    <play>Hamlet</play>
    //    <audience>55</audience>
    //    <amount>650.00</amount>
    //  </performance>
    //  <performance>
    //    <play>As You Like It</play>
    //    <audience>35</audience>
    //    <amount>580.00</amount>
    //  </performance>
    //  <performance>
    //    <play>Othello</play>
    //    <audience>40</audience>
    //    <amount>500.00</amount>
    //  </performance>
    //  <total>1730.00</total>
    //  <credits>47</credits>
    //</statement>", result);
    //        }

    private void CalculatePerformanceCostAndCredits(Performance performance, IGenreStrategy strategyGenreFactory)
    {


        performance.Cost = strategyGenreFactory.CalculateCost(performance.Audience, performance.Play.Lines);
        performance.Credits = strategyGenreFactory.CalculateCredits(performance.Audience);
    }

}
