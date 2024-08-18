using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using ApprovalTests;
using ApprovalTests.Reporters;
using TheatricalPlayersRefactoringKata.Enums;
using Xunit;

namespace TheatricalPlayersRefactoringKata.Tests
{
    public class StatementPrinterTests
    {
        private static async Task<Statement> CreateAndProcessStatementAsync(Invoice invoice, Dictionary<string, Play> plays)
        {
            // Output directory for saving the statement
            var outputDirectory = "outputTest";
            // Ensure the output directory exists
            if (!Directory.Exists(outputDirectory)) Directory.CreateDirectory(outputDirectory);
            // Create a new StatementProcessor with the plays and output directory
            var statementProcessor = new StatementProcessor(plays, outputDirectory);
            // Process the invoice to generate the Statement
            var statement = await statementProcessor.ProcessInvoiceAsync(invoice);
            // Save the generated Statement as XML
            await statementProcessor.SaveXmlAsync(statement);
            // Return the Statement for verification in the tests
            return statement;
        }

        [Fact]
        [UseReporter(typeof(DiffReporter))]
        public async Task TestStatementExampleLegacy()
        {
            var plays = new Dictionary<string, Play>
            {
                { "hamlet", new Play("Hamlet", 4024, TheatricalType.Tragedy) },
                { "as-like", new Play("As You Like It", 2670, TheatricalType.Comedy) },
                { "othello", new Play("Othello", 3560, TheatricalType.Tragedy) }
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
            // Create and process the statement based on the invoice and plays
            Statement statement = await CreateAndProcessStatementAsync(invoice, plays);
            // Generate the text format of the statement
            string result = StatementPrinter.TxtPrint(statement);
            // Verify the result against the approved version
            Approvals.Verify(result);
        }

        [Fact]
        [UseReporter(typeof(DiffReporter))]
        public async Task TestTextStatementExample()
        {
            var plays = new Dictionary<string, Play>
            {
                { "hamlet", new Play("Hamlet", 4024, TheatricalType.Tragedy) },
                { "as-like", new Play("As You Like It", 2670, TheatricalType.Comedy) },
                { "othello", new Play("Othello", 3560, TheatricalType.Tragedy) },
                { "henry-v", new Play("Henry V", 3227, TheatricalType.History) },
                { "john", new Play("King John", 2648, TheatricalType.History) },
                { "richard-iii", new Play("Richard III", 3718, TheatricalType.History) }
            };
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
            // Create and process the statement based on the invoice and plays
            Statement statement = await CreateAndProcessStatementAsync(invoice, plays);
            // Generate the text format of the statement
            string result = StatementPrinter.TxtPrint(statement);
            // Verify the result against the approved version
            Approvals.Verify(result);
        }

        [Fact]
        [UseReporter(typeof(DiffReporter))]
        public async Task TestXmlStatementExample()
        {
            var plays = new Dictionary<string, Play>
            {
                { "hamlet", new Play("Hamlet", 4024, TheatricalType.Tragedy) },
                { "as-like", new Play("As You Like It", 2670, TheatricalType.Comedy) },
                { "othello", new Play("Othello", 3560, TheatricalType.Tragedy) },
                { "henry-v", new Play("Henry V", 3227, TheatricalType.History) },
                { "john", new Play("King John", 2648, TheatricalType.History) },
                { "richard-iii", new Play("Richard III", 3718, TheatricalType.History) }
            };
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
            // Create and process the statement based on the invoice and plays
            Statement statement = await CreateAndProcessStatementAsync(invoice, plays);
            // Generate the XML format of the statement
            string result = StatementPrinter.XmlPrint(statement);
            // Verify the result against the approved version
            Approvals.Verify(result);
        }
    }
}
