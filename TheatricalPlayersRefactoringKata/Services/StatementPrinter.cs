using System;
using System.Collections.Generic;
using System.Globalization;
using TheatricalPlayersRefactoringKata.Models;

namespace TheatricalPlayersRefactoringKata.Services;

public class StatementPrinter
{
    private readonly Dictionary<string, IPerformanceCalculator> _calculators;

    public StatementPrinter()
    {
        _calculators = new Dictionary<string, IPerformanceCalculator>
        {
            { "tragedy", new TragedyCalculator() },
            { "comedy", new ComedyCalculator() },
            { "history", new HistoryCalculator() }
        };
    }

    public string Print(Invoice invoice, Dictionary<string, Play> plays)
    {
        var totalAmount = 0;
        var volumeCredits = 0;
        var result = $"Statement for {invoice.Customer}\n";

        CultureInfo cultureInfo = new CultureInfo("en-US");

        foreach (var perf in invoice.Performances)
        {
            var play = plays[perf.PlayId];
            var calculator = _calculators[play.Type];

            var thisAmount = calculator.CalculateAmount(perf, play);
            volumeCredits += calculator.CalculateVolumeCredits(perf, play);

            result += $"{play.Name}: {Convert.ToDecimal(thisAmount / 100):C} ({perf.Audience} seats)\n";
            totalAmount += thisAmount;
        }

        result += $"Amount owed is {Convert.ToDecimal(totalAmount / 100):C}\n";
        result += $"You earned {volumeCredits} credits\n";

        return result;
    }
}
