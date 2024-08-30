using System.Collections.Generic;
using TheatricalPlayersRefactoringKata.Application.Interfaces;
using TheatricalPlayersRefactoringKata.Application.Strategies;
using TheatricalPlayersRefactoringKata.Models;

namespace TheatricalPlayersRefactoringKata.Application.Adapters;

public class JsonFormatterAdapter : IFormatterAdapter
{
    private readonly CalculationStrategyFactory _strategyFactory;

    public JsonFormatterAdapter(CalculationStrategyFactory strategyFactory)
    {
        _strategyFactory = strategyFactory;
    }

    public string Format(Invoice invoice, Dictionary<string, Play> plays, decimal totalAmount, decimal volumeCredits)
    {
        // TODO: Trabalhar na logica JSON
        return string.Empty;
    }
}
