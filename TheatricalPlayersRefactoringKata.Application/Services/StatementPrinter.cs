using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Application.Interfaces;
using TheatricalPlayersRefactoringKata.Application.Constants;
using TheatricalPlayersRefactoringKata.Domain;
using TheatricalPlayersRefactoringKata.Domain.Entities;
using TheatricalPlayersRefactoringKata.Domain.Exceptions;
using TheatricalPlayersRefactoringKata.Application.Strategy;
using static TheatricalPlayersRefactoringKata.Domain.Entities.InvoicePrint;
using TheatricalPlayersRefactoringKata.Domain.Enum;

namespace TheatricalPlayersRefactoringKata.Application
{
    /// <summary>
    /// Service class for generating and printing invoices.
    /// Handles invoice processing, including calculating amounts and credits, and outputting the results in different formats.
    /// </summary>
    public class StatementPrinter
    {
        private readonly ICalculateBaseAmountPerLine _calculateBaseAmountPerLine;
        private readonly ICalculateCreditAudience _calculateCreditAudience;
        private readonly ICalculateAdditionalValuePerPlayType _calculateAdditionalValuePerPlayType;
        private readonly IInvoicePrintFactory _invoicePrintFactory;
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly Queue<Invoice> _invoiceQueue;
        private readonly object _queueLock = new object();

        /// <summary>
        /// Initializes a new instance of the <see cref="StatementPrinter"/> class.
        /// </summary>
        /// <param name="calculateBaseAmountPerLine">Service to calculate base amount per line.</param>
        /// <param name="calculateCreditAudience">Service to calculate credit based on audience size.</param>
        /// <param name="calculateAdditionalValuePerPlayType">Service to calculate additional value per play type.</param>
        /// <param name="invoicePrintFactory">Factory for creating invoice print strategies.</param>
        /// <param name="invoiceRepository">Repository for accessing invoice and related data.</param>
        public StatementPrinter(ICalculateBaseAmountPerLine calculateBaseAmountPerLine,
                                ICalculateCreditAudience calculateCreditAudience,
                                ICalculateAdditionalValuePerPlayType calculateAdditionalValuePerPlayType,
                                IInvoicePrintFactory invoicePrintFactory,
                                IInvoiceRepository invoiceRepository)
        {
            _calculateBaseAmountPerLine = calculateBaseAmountPerLine;
            _calculateCreditAudience = calculateCreditAudience;
            _calculateAdditionalValuePerPlayType = calculateAdditionalValuePerPlayType;
            _invoicePrintFactory = invoicePrintFactory;
            _invoiceRepository = invoiceRepository;
            _invoiceQueue = new Queue<Invoice>();
        }

        /// <summary>
        /// Generates a formatted invoice based on the provided invoice ID and print type request.
        /// </summary>
        /// <param name="invoiceId">The ID of the invoice to print.</param>
        /// <param name="printTypeRequest">The type of print format requested (e.g., XML).</param>
        /// <returns>The generated invoice as a formatted string.</returns>
        public string PrintInvoice(string invoiceId, string printTypeRequest)
        {
            var invoice = GetInvoice(invoiceId);
            var performances = GetPerformances(invoiceId);
            var plays = GetAllPlays();
            var calcSettings = GetInvoiceCalculationSettings();
            var creditSettings = GetInvoiceCreditSettings();
            var printType = GetPrintType(printTypeRequest);

            return PrintInvoice(invoice, performances, plays, printType, calcSettings, creditSettings);
        }

        /// <summary>
        /// Retrieves an invoice by its ID from the repository.
        /// </summary>
        /// <param name="invoiceId">The ID of the invoice to retrieve.</param>
        /// <returns>The retrieved <see cref="Invoice"/> object.</returns>
        /// <exception cref="InvoiceNotFoundException">Thrown if the invoice is not found.</exception>
        private Invoice GetInvoice(string invoiceId)
        {
            var invoice = _invoiceRepository.GetInvoiceById(invoiceId);
            if (invoice == null) throw new InvoiceNotFoundException(invoiceId);
            return invoice;
        }

        /// <summary>
        /// Retrieves the list of performances associated with a specific invoice ID.
        /// </summary>
        /// <param name="invoiceId">The ID of the invoice whose performances are to be retrieved.</param>
        /// <returns>A list of <see cref="Performance"/> objects.</returns>
        /// <exception cref="PerformanceNotFoundException">Thrown if performances are not found.</exception>
        private List<Performance> GetPerformances(string invoiceId)
        {
            var performances = _invoiceRepository.GetPerformancesByInvoiceId(invoiceId);
            if (performances == null) throw new PerformanceNotFoundException(invoiceId);
            return performances;
        }

        /// <summary>
        /// Retrieves all available plays from the repository.
        /// </summary>
        /// <returns>A list of <see cref="Play"/> objects.</returns>
        private List<Play> GetAllPlays()
        {
            return _invoiceRepository.GetAllPlays();
        }

        /// <summary>
        /// Retrieves invoice calculation settings from the repository.
        /// </summary>
        /// <returns>A list of <see cref="InvoiceCalculeteSettings"/> objects.</returns>
        private List<InvoiceCalculeteSettings> GetInvoiceCalculationSettings()
        {
            return _invoiceRepository.GetInvoiceCalculeteSettings();
        }

        /// <summary>
        /// Retrieves invoice credit settings from the repository.
        /// </summary>
        /// <returns>A list of <see cref="InvoiceCreditSettings"/> objects.</returns>
        private List<InvoiceCreditSettings> GetInvoiceCreditSettings()
        {
            return _invoiceRepository.GetInvoiceCreditSettings();
        }

        /// <summary>
        /// Determines the print type based on the requested print type string.
        /// </summary>
        /// <param name="printTypeRequest">The requested print type as a string.</param>
        /// <returns>The corresponding <see cref="PrintType"/> enumeration value.</returns>
        private PrintType GetPrintType(string printTypeRequest)
        {
            return _invoicePrintFactory.DeterminePrintType(printTypeRequest);
        }

        /// <summary>
        /// Generates a formatted invoice based on the provided data and print type.
        /// </summary>
        /// <param name="invoice">The invoice object to print.</param>
        /// <param name="performances">The list of performances associated with the invoice.</param>
        /// <param name="plays">The list of all plays.</param>
        /// <param name="printType">The type of print format requested.</param>
        /// <param name="invoiceCalculeteSettings">The settings for calculating invoice amounts.</param>
        /// <param name="creditSettings">The settings for calculating credits.</param>
        /// <returns>The generated invoice as a formatted string.</returns>
        public string PrintInvoice(Invoice invoice, List<Performance> performances, List<Play> plays, PrintType printType,
                                    List<InvoiceCalculeteSettings> invoiceCalculeteSettings, List<InvoiceCreditSettings> creditSettings)
        {
            var invoicePrint = new InvoicePrint.Statement
            {
                Customer = invoice.Customer,
                Items = new Items()
            };
            invoicePrint.Items.Item = new List<InvoicePrint.Item>();

            var totalAmount = CalculateTotalAmount(performances, plays, invoiceCalculeteSettings, creditSettings, invoicePrint);
            var totalCredits = CalculateTotalCredits(performances, plays, creditSettings);

            invoicePrint.AmountOwed = totalAmount;
            invoicePrint.EarnedCredits = totalCredits;

            var printer = _invoicePrintFactory.GetPrintType(printType);
            return printer.Print(invoicePrint);
        }

        /// <summary>
        /// Calculates the total amount owed for the invoice based on performances, plays, and settings.
        /// </summary>
        /// <param name="performances">The list of performances.</param>
        /// <param name="plays">The list of all plays.</param>
        /// <param name="calcSettings">The settings for calculating invoice amounts.</param>
        /// <param name="creditSettings">The settings for calculating credits.</param>
        /// <param name="invoicePrint">The invoice print statement to populate with items.</param>
        /// <returns>The total amount owed as a <see cref="decimal"/>.</returns>
        private decimal CalculateTotalAmount(List<Performance> performances, List<Play> plays,
                                             List<InvoiceCalculeteSettings> calcSettings, List<InvoiceCreditSettings> creditSettings,
                                             InvoicePrint.Statement invoicePrint)
        {
            decimal totalAmount = 0;

            foreach (var performance in performances)
            {
                var play = FindPlayById(performance.PlayId, plays);
                if (play == null) throw new PlayNotFoundException(performance.PlayId.ToString());

                decimal playAmount = CalculatePlayAmount(play, performance, calcSettings);
                decimal earnedCredits = CalculateEarnedCredits(play, performance, creditSettings);

                totalAmount += playAmount;

                invoicePrint.Items.Item.Add(new InvoicePrint.Item
                {
                    Name = play.Name,
                    AmountOwed = playAmount,
                    Seats = performance.Audience,
                    EarnedCredits = earnedCredits
                });
            }

            return totalAmount;
        }

        /// <summary>
        /// Calculates the amount owed for a specific play and performance.
        /// </summary>
        /// <param name="play">The play object.</param>
        /// <param name="performance">The performance object.</param>
        /// <param name="calcSettings">The settings for calculating invoice amounts.</param>
        /// <returns>The amount owed for the play as a <see cref="decimal"/>.</returns>
        private decimal CalculatePlayAmount(Play play, Performance performance, List<InvoiceCalculeteSettings> calcSettings)
        {
            var playSettings = calcSettings.Where(x => x.PlayTypeId == play.PlayTypeId).ToList();
            decimal playAmount = 0;

            foreach (var setting in playSettings)
            {
                playAmount += _calculateBaseAmountPerLine.CalculateBaseAmount(play.Lines);
                playAmount += _calculateAdditionalValuePerPlayType.CalculateAdditionalValue(
                    performance.Audience,
                    setting.MinimumAudience,
                    setting.Bonus,
                    setting.PerAudienceAdditional,
                    setting.PerAudience
                );
            }

            return playAmount;
        }

        /// <summary>
        /// Calculates the credits earned for a specific play and performance.
        /// </summary>
        /// <param name="play">The play object.</param>
        /// <param name="performance">The performance object.</param>
        /// <param name="creditSettings">The settings for calculating credits.</param>
        /// <returns>The credits earned as a <see cref="decimal"/>.</returns>
        private decimal CalculateEarnedCredits(Play play, Performance performance, List<InvoiceCreditSettings> creditSettings)
        {
            var creditSetting = creditSettings.FirstOrDefault(x => x.PlayTypeId == play.PlayTypeId);
            return _calculateCreditAudience.CalculateCredit(performance.Audience, creditSetting);
        }

        /// <summary>
        /// Calculates the total credits earned for all performances.
        /// </summary>
        /// <param name="performances">The list of performances.</param>
        /// <param name="plays">The list of all plays.</param>
        /// <param name="creditSettings">The settings for calculating credits.</param>
        /// <returns>The total credits earned as a <see cref="decimal"/>.</returns>
        private decimal CalculateTotalCredits(List<Performance> performances, List<Play> plays, List<InvoiceCreditSettings> creditSettings)
        {
            decimal totalCredits = 0;

            foreach (var performance in performances)
            {
                var play = FindPlayById(performance.PlayId, plays);
                totalCredits += CalculateEarnedCredits(play, performance, creditSettings);
            }

            return totalCredits;
        }

        /// <summary>
        /// Finds a play by its ID in the list of plays.
        /// </summary>
        /// <param name="playId">The ID of the play to find.</param>
        /// <param name="plays">The list of all plays.</param>
        /// <returns>The found <see cref="Play"/> object.</returns>
        /// <exception cref="PlayNotFoundException">Thrown if the play is not found.</exception>
        private Play FindPlayById(int playId, List<Play> plays)
        {
            var play = plays.FirstOrDefault(x => x.PlayId == playId);
            if (play == null) throw new PlayNotFoundException(playId.ToString());
            return play;
        }

        /// <summary>
        /// Adds an invoice to the processing queue.
        /// </summary>
        /// <param name="invoice">The invoice to add to the queue.</param>
        public void EnqueueInvoiceForProcessing(Invoice invoice)
        {
            lock (_queueLock)
            {
                _invoiceQueue.Enqueue(invoice);
            }
        }

        /// <summary>
        /// Processes invoices from the queue asynchronously, generating XML files for each invoice.
        /// </summary>
        /// <param name="path">The directory path where the XML files will be saved.</param>
        public async Task ProcessInvoicesAsync(string path)
        {
            while (true)
            {
                Invoice invoice = null;
                lock (_queueLock)
                {
                    if (_invoiceQueue.Count > 0)
                    {
                        invoice = _invoiceQueue.Dequeue();
                    }
                }

                if (invoice != null)
                {
                    await ProcessInvoiceAsync(invoice, path);
                }
                else
                {
                    await Task.Delay(1000);
                }
            }
        }

        /// <summary>
        /// Processes a single invoice asynchronously and writes its XML representation to a file.
        /// </summary>
        /// <param name="invoice">The invoice to process.</param>
        /// <param name="path">The directory path where the XML file will be saved.</param>
        private async Task ProcessInvoiceAsync(Invoice invoice, string path)
        {
            var performances = await _invoiceRepository.GetPerformancesByInvoiceIdAsync(invoice.InvoiceId.ToString());
            var plays = await _invoiceRepository.GetAllPlaysAsync();
            var calcSettings = await _invoiceRepository.GetInvoiceCalculeteSettingsAsync();
            var invoiceCreditSettings = await _invoiceRepository.GetInvoiceCreditSettingsAsync();

            var result = PrintInvoice(invoice, performances, plays, PrintType.XML, calcSettings, invoiceCreditSettings);

            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
            var xmlPath = $@"{path}\{invoice.InvoiceId}.xml";
            await File.WriteAllTextAsync(xmlPath, result);
        }
    }
}
