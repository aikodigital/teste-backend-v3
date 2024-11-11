using System;
using System.Collections.Generic;
using System.IO;
using TheatricalPlayersRefactoringKata.Model.Models;
using TheatricalPlayersRefactoringKata.Service.Printer;
using Xunit;

namespace TheatricalPlayersRefactoringKata.Tests.IntegrationTests;

public class XmlFileExporterTests
{
    [Fact]
    public async void ExportXmlTest()
    {
        var plays = new Dictionary<string, Play>()
        {
            {"hamlet", new Play("Hamlet", 4024, Genre.tragedy)},
            {"as-like", new Play("As You Like It", 2670, Genre.comedy)},
            {"othello", new Play("Othello", 3560, Genre.tragedy)},
        };

        Invoice invoice = new Invoice(
            "BigCo",
            new List<Performance>
            {
                new Performance("hamlet", 55),
                new Performance("as-like", 35),
                new Performance("othello", 40),
            }
        );

        XmlStatementPrinter statementPrinter = new XmlStatementPrinter();
        string filePath = AppDomain.CurrentDomain.BaseDirectory + "/statementXml.xml";
        await statementPrinter.PrintAndExport(invoice, plays, filePath);
        
        Assert.True(File.Exists(filePath), $"O arquivo XML não foi criado em {filePath}");
        
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }
    }
}
