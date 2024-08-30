using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Core.Entities;

namespace TheatricalPlayersRefactoringKata.Application.Services.Statment;

public class TextStatementFormatter : IStatementFormatter
{
    private readonly IInvoiceCalculationService _invoiceCalculationService;
    private readonly Dictionary<string, IPerformanceCalculator> _calculators;

    public TextStatementFormatter(IInvoiceCalculationService invoiceCalculationService, Dictionary<string, IPerformanceCalculator> calculators)
    {
        _invoiceCalculationService = invoiceCalculationService;
        _calculators = calculators;
    }

    public async Task<string> FormatAsync(Invoice invoice)
    {
        var result = new StringBuilder($"Statement for {invoice.Customer}\n");

        foreach (var performance in invoice.Performances)
        {
            var play = performance.Play;
            var calculator = _calculators[play.Type];

            decimal amount = await calculator.CalculateAmount(performance, play);
            int volumeCredits = await calculator.CalculateVolumeCredits(performance, play);

            result.AppendLine($"{play.Name}: {amount:C2} ({performance.Audience} seats)");

            invoice.TotalVolumeCredits += volumeCredits;
        }

        decimal totalAmount = await _invoiceCalculationService.CalculateTotalValue(invoice);

        result.AppendLine($"Amount owed is {totalAmount:C2}");
        result.AppendLine($"You earned {invoice.TotalVolumeCredits} credits");

        return result.ToString();
    }
}
