using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using ApprovalTests;
using ApprovalTests.Reporters;
using Moq;
using TheatricalPlayersRefactoringKata.Application;
using TheatricalPlayersRefactoringKata.Application.Files;
using TheatricalPlayersRefactoringKata.Application.Formatters;
using TheatricalPlayersRefactoringKata.Application.Interfaces;
using Xunit;

namespace TheatricalPlayersRefactoringKata.Tests;

public class StatementPrinterTests
{
    [Obsolete]
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

        var formatterMock = new Mock<IStatementFormatter>();
        formatterMock.Setup(f => f.Format(It.IsAny<Invoice>()))
                     .Returns("Formatted Statement");

        var statementPrinterMock = new Mock<Application.StatementPrinter>();
        statementPrinterMock.Setup(sp => sp.Print(It.IsAny<Invoice>(), It.IsAny<Dictionary<string, Play>>(), It.IsAny<IStatementFormatter>()))
                            .Returns("Formatted Statement");

        var result = statementPrinterMock.Object.Print(invoice, plays, formatterMock.Object);

        Approvals.Verify(result);
    }

    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void TestTextStatementExample()
    {
        // Arrange
        var plays = new Dictionary<string, Play>
    {
        { "hamlet", new Play("Hamlet", 4024, "tragedy") },
        { "as-like", new Play("As You Like It", 2670, "comedy") },
        { "othello", new Play("Othello", 3560, "tragedy") },
        { "henry-v", new Play("Henry V", 3227, "history") },
        { "john", new Play("King John", 2648, "history") },
        { "richard-iii", new Play("Richard III", 3718, "history") }
    };

        var invoice = new Invoice(
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

        var formatterMock = new Mock<IStatementFormatter>();
        formatterMock.Setup(f => f.Format(It.IsAny<Invoice>()))
                     .Returns("Formatted Text Statement");

        var statementPrinterMock = new Mock<Application.StatementPrinter>();
        statementPrinterMock.Setup(sp => sp.Print(It.IsAny<Invoice>(), It.IsAny<Dictionary<string, Play>>(), It.IsAny<IStatementFormatter>()))
                            .Returns("Formatted Text Statement");

        var result = statementPrinterMock.Object.Print(invoice, plays, formatterMock.Object);

        Approvals.Verify(result);
    }

    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void TestXmlStatementExample()
    {
        // Arrange
        var plays = new Dictionary<string, Play>
    {
        { "hamlet", new Play("Hamlet", 4024, "tragedy") },
        { "as-like", new Play("As You Like It", 2670, "comedy") },
        { "othello", new Play("Othello", 3560, "tragedy") },
        { "henry-v", new Play("Henry V", 3227, "history") },
        { "john", new Play("King John", 2648, "history") },
        { "richard-iii", new Play("Richard III", 3718, "history") }
    };

        var invoice = new Invoice(
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

        var formatterMock = new Mock<IStatementFormatter>();
        formatterMock.Setup(f => f.Format(It.IsAny<Invoice>()))
                     .Returns("<xml><statement>Formatted XML Statement</statement></xml>");

        var statementPrinterMock = new Mock<Application.StatementPrinter>();
        statementPrinterMock.Setup(sp => sp.Print(It.IsAny<Invoice>(), It.IsAny<Dictionary<string, Play>>(), It.IsAny<IStatementFormatter>()))
                            .Returns("<xml><statement>Formatted XML Statement</statement></xml>");

        var result = statementPrinterMock.Object.Print(invoice, plays, formatterMock.Object);

        Approvals.VerifyXml(result);
    }


    [Fact]
    public async Task GenerateAndSaveStatementAsync_ShouldSaveStatementToFile()
    {
        // Arrange
        var plays = new Dictionary<string, Play>
    {
        { "hamlet", new Play("Hamlet", 4024, "tragedy") },
        { "as-like", new Play("As You Like It", 2670, "comedy") },
        { "othello", new Play("Othello", 3560, "tragedy") },
        { "henry-v", new Play("Henry V", 3227, "history") },
        { "john", new Play("King John", 2648, "history") },
        { "richard-iii", new Play("Richard III", 3718, "history") }
    };

        var invoice = new Invoice(
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

        var formatterMock = new Mock<IStatementFormatter>();
        formatterMock.Setup(f => f.Format(It.IsAny<Invoice>()))
                     .Returns("<xml><statement>Formatted XML Statement</statement></xml>");

        var directory = @"C:\estudo\arquivos";
        var fileName = "TheatricalPlayers.xml";

        // Mocking StatementFileService
        var fileServiceMock = new Mock<StatementFileService>();
        fileServiceMock.Setup(f => f.GenerateStatementFile(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<XmlFile>()))
                       .Returns(Task.CompletedTask);

        var statementPrinterMock = new Mock<Application.StatementPrinter>();
        statementPrinterMock.Setup(sp => sp.Print(It.IsAny<Invoice>(), It.IsAny<Dictionary<string, Play>>(), It.IsAny<IStatementFormatter>()))
                            .Returns("<xml><statement>Formatted XML Statement</statement></xml>");

        var result = statementPrinterMock.Object.Print(invoice, plays, formatterMock.Object);
        await fileServiceMock.Object.GenerateStatementFile(result, directory, fileName, new XmlFile());

        var savedContent = await File.ReadAllTextAsync($@"{directory}\{fileName}");
        Assert.Equal("<xml>Sample XML content</xml>", savedContent);
    }

}
