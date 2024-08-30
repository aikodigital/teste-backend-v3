using System.Collections.Generic;
using System.Globalization;
using System;
using TheatricalPlayersRefactoringKata.Application.Interfaces;
using TheatricalPlayersRefactoringKata.Application.Strategies;
using TheatricalPlayersRefactoringKata.Models;

namespace TheatricalPlayersRefactoringKata.Application.Adapters;

public class TextFormatterAdapter : IFormatterAdapter
{
    private readonly CalculationStrategyFactory _strategyFactory;

    public TextFormatterAdapter(CalculationStrategyFactory strategyFactory)
    {
        _strategyFactory = strategyFactory;
    }

    public string Format(Invoice invoice, Dictionary<string, Play> plays, decimal totalAmount, decimal volumeCredits)
    {
        var cultureInfo = new CultureInfo("en-US");
        var result = $"Statement for {invoice.Customer}\n";

        foreach (var perf in invoice.Performances)
        {
            var play = plays[perf.PlayId];
            var strategy = _strategyFactory.GetStrategy(play.Type);
            var thisAmount = strategy.CalculateAmount(perf, play);
            volumeCredits += strategy.CalculateCredits(perf, play);

            result += string.Format(cultureInfo, "  {0}: {1:C} ({2} seats)\n", play.Name, Convert.ToDecimal(thisAmount / 100), perf.Audience);
            totalAmount += thisAmount;
        }

        result += string.Format(cultureInfo, "Amount owed is {0:C}\n", Convert.ToDecimal(totalAmount / 100));
        result += $"You earned {volumeCredits} credits\n";

        return result;
    }
}

