using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using TheatricalPlayersRefactoringKata.Application.Services;
using TheatricalPlayersRefactoringKata.Core.Entities;

public class StatementPrinter
{
    private readonly IInvoiceCalculationService _invoiceCalculationService;
    private readonly Dictionary<string, IPerformanceCalculator> _calculators;

    public StatementPrinter(IInvoiceCalculationService invoiceCalculationService, Dictionary<string, IPerformanceCalculator> calculators)
    {
        Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
        Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
        _invoiceCalculationService = invoiceCalculationService;
        _calculators = calculators;
    }

    public async Task<string> PrintAsync(Invoice invoice)
    {
        var culture = new CultureInfo("en-US");
        var result = new StringBuilder($"Statement for {invoice.Customer}\n");
        decimal totalAmount = 0;
        int totalCredits = 0;

        foreach (var performance in invoice.Performances)
        {
            var play = performance.Play;
            var calculator = _calculators[play.Type];

            decimal amount = await calculator.CalculateAmount(performance, play);
            int credits = await calculator.CalculateVolumeCredits(performance, play);

            result.AppendLine($"  {play.Name}: {amount.ToString("C2", culture)} ({performance.Audience} seats)");
            totalAmount += amount;
            totalCredits += credits;
        }

        result.AppendLine($"Amount owed is {totalAmount.ToString("C2", culture)}");
        result.AppendLine($"You earned {totalCredits} credits");

        return result.ToString();
    }
    public async Task<string> PrintAsXmlAsync(Invoice invoice)
    {
        var culture = new CultureInfo("en-US");

        var performancesXml = new XElement("performances");

        foreach (var performance in invoice.Performances)
        {
            var play = performance.Play;
            var calculator = _calculators[play.Type];

            decimal amount = await calculator.CalculateAmount(performance, play);
            int volumeCredits = await calculator.CalculateVolumeCredits(performance, play);

            var performanceXml = new XElement("performance",
                new XElement("playName", play.Name),
                new XElement("audience", performance.Audience),
                new XElement("amount", amount.ToString("C2", culture)),
                new XElement("volumeCredits", volumeCredits)
            );

            performancesXml.Add(performanceXml);

            // Acumula os créditos
            invoice.TotalVolumeCredits += volumeCredits;
        }

        decimal totalAmount = await _invoiceCalculationService.CalculateTotalValue(invoice);

        var invoiceXml = new XElement("invoice",
            new XElement("customer", invoice.Customer),
            performancesXml,
            new XElement("totalAmount", totalAmount.ToString("C2", culture)),
            new XElement("totalVolumeCredits", invoice.TotalVolumeCredits)
        );

        var document = new XDocument(invoiceXml);
        return document.ToString();
    }
}
