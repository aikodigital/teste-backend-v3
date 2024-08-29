using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Core.Entities;

namespace TheatricalPlayersRefactoringKata.Application.Services;

public class InvoiceCalculationService : IInvoiceCalculationService
{
    private readonly Dictionary<string, IPerformanceCalculator> _calculators;
    public InvoiceCalculationService(Dictionary<string, IPerformanceCalculator> calculators)
    {
        _calculators = calculators;
    }

    public async Task<decimal> CalculateTotalValue(Invoice invoice)
    {
        decimal totalAmount = 0;

        foreach (var performance in invoice.Performances)
        {
            var play = performance.Play;
            var calculator = _calculators[play.Type];

            // Calcular o valor para a performance usando o calculador apropriado
            decimal amount = await calculator.CalculateAmount(performance, play);
            totalAmount += amount;
        }

        return totalAmount;
    }

    private int CalculateBaseAmount(Play play, int audience)
    {
        int baseAmount = (play.Lines / 10);
        baseAmount = Clamp(baseAmount, 100, 400);

        switch (play.Type)
        {
            case "tragedy":
                baseAmount += (audience > 30) ? 10 * (audience - 30) : 0;
                break;

            case "comedy":
                baseAmount += 3 * audience;
                if (audience > 20)
                {
                    baseAmount += 100 + 5 * (audience - 20);
                }
                break;
        }

        return baseAmount;
    }

    private int Clamp(int value, int min, int max)
    {
        return (value < min) ? min : (value > max) ? max : value;
    }
}
