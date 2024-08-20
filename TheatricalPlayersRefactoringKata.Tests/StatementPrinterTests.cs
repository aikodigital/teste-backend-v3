using System;
using System.Collections.Generic;
using System.IO;
using ApprovalTests;
using ApprovalTests.Reporters;
using Xunit;

namespace TheatricalPlayersRefactoringKata.Tests;

public class StatementPrinterTests
{

    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void TestTextStatementExample()
    {
        // Arrange: Set up the data needed for the test
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

        // Act: Create an instance of StatementPrinter and generate the statement
        StatementPrinter statementPrinter = new StatementPrinter();
        var result = statementPrinter.Print(invoice, plays);

        // Assert: Verify that the generated result matches the expected output
        Approvals.Verify(result);
    }


    [Fact]
    public void TestXmlStatementExample()
    {
        // Arrange: Set up the data needed for the test
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

        // Act: Create an instance of StatementPrinter and save the XML to a file
        var statementPrinter = new StatementPrinter();
        var tempFilePath = Path.Combine(Path.GetTempPath(), "Statement.xml"); // Define a temporary file path with a specific name

        try
        {
            // Ensure the file does not exist before the test
            if (File.Exists(tempFilePath))
            {
                File.Delete(tempFilePath);
            }

            // Save the XML to file
            statementPrinter.SaveXmlToFile(invoice, plays, tempFilePath);

            // Assert that the file was created
            Assert.True(File.Exists(tempFilePath), "The XML file was not created.");

            // Optionally, verify the file size to ensure it is not empty (a basic check)
            var fileInfo = new FileInfo(tempFilePath);
            Assert.True(fileInfo.Length > 0, "The XML file is empty.");
        }
        finally
        {
            // Clean up: delete the file after the test
            if (File.Exists(tempFilePath))
            {
                File.Delete(tempFilePath);
            }
        }
    }
}










