using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Enums;
using TheatricalPlayersRefactoringKata.TheatricalGenre;

namespace TheatricalPlayersRefactoringKata
{
    public class StatementProcessor
    {
        private ConcurrentQueue<Invoice> _invoiceQueue;
        private Dictionary<string, Play> _plays;
        private string _outputDirectory;

        /// <summary>
        /// Represents a statement processor for calculating and saving statements
        /// </summary>
        /// <param name="plays"> Dictionary of plays </param>
        /// <param name="outputDirectory"> Output directory for saving statements </param>
        /// <exception cref="ArgumentNullException"> Thrown when the plays or output directory is null </exception>
        public StatementProcessor(Dictionary<string, Play> plays, string outputDirectory)
        {
            _invoiceQueue = new ConcurrentQueue<Invoice>();
            _plays = plays ?? throw new ArgumentNullException(nameof(plays));
            _outputDirectory = outputDirectory ?? throw new ArgumentNullException(nameof(outputDirectory));

            // Ensure the output directory exists
            if (!Directory.Exists(_outputDirectory))
            {
                Directory.CreateDirectory(_outputDirectory);
            }
        }

        /// <summary>
        /// Queue an invoice for processing
        /// </summary>
        /// <param name="invoice"> Invoice to queue </param>
        /// <exception cref="ArgumentNullException"> Thrown when the invoice is null </exception>
        public void QueueInvoice(Invoice invoice)
        {
            if (invoice == null) throw new ArgumentNullException(nameof(invoice));
            _invoiceQueue.Enqueue(invoice);
        }

        /// <summary>
        /// Process all queued invoices
        /// </summary>
        /// <returns></returns>
        public async Task ProcessInvoicesAsync()
        {
            while (_invoiceQueue.TryDequeue(out var invoice))
            {
                var statement = await ProcessInvoiceAsync(invoice);
                await SaveXmlAsync(statement);
            }
        }

        public async Task<Statement> ProcessInvoiceAsync(Invoice invoice)
        {
            return await Task.Run(() =>
            {
                var statement = new Statement(invoice.Customer);

                foreach (var perf in invoice.Performances)
                {
                    Play play = _plays[perf.PlayId];
                    int lines = play.Lines;
                    Genre genre = play.Type switch
                    {
                        TheatricalType.Tragedy => new Tragedy(),
                        TheatricalType.Comedy => new Comedy(),
                        TheatricalType.History => new History(),
                        _ => throw new Exception($"Unknown theatrical genre {play.Type}")
                    };

                    decimal thisAmount = Convert.ToDecimal(genre.CalculateAmount(perf.Audience, lines) / 100m);
                    int credits = genre.CalculateVolumeCredits(perf.Audience);

                    statement.Lines.Add(new StatementLine(play.Name, thisAmount, credits, perf.Audience));
                    statement.TotalAmount += thisAmount;
                    statement.VolumeCredits += credits;
                }

                return statement;
            });
        }

        public async Task SaveXmlAsync(Statement statement)
        {
            await Task.Run(() =>
            {
                // Use the existing XmlPrint method to generate the XML string
                string xmlContent = StatementPrinter.XmlPrint(statement);
                // Define the output file path
                var outputPath = Path.Combine(_outputDirectory, $"{statement.Customer}_statement.xml");
                // Save the XML content to the file
                File.WriteAllText(outputPath, xmlContent, Encoding.UTF8);
            });
        }
    }
}
