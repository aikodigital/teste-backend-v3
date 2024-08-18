using System.Collections.Generic;
using System.Globalization;
using TheatricalPlayersRefactoringKata;

public class StatementPrinter
{
    private readonly Dictionary<string, IPlayCategory> _playCategories;

    public StatementPrinter(Dictionary<string, IPlayCategory> playCategories)
    {
        _playCategories = playCategories;
    }

    public string Print(Invoice invoice, Dictionary<string, Play> plays)
    {
        var totalAmount = 0m;
        var volumeCredits = 0;
        var result = $"Statement for {invoice.Customer}\n";
        CultureInfo cultureInfo = new CultureInfo("en-US");

        foreach (var perf in invoice.Performances)
        {
            var play = plays[perf.PlayId];
            var category = _playCategories[play.Type];
            var thisAmount = category.CalculateAmount(perf.Audience, play.Lines);
            var thisCredits = category.CalculateCredits(perf.Audience);

            volumeCredits += thisCredits;

            result += string.Format(cultureInfo, "  {0}: {1:C} ({2} seats)\n", play.Name, thisAmount, perf.Audience);
            totalAmount += thisAmount;
        }

        result += string.Format(cultureInfo, "Amount owed is {0:C}\n", totalAmount);
        result += $"You earned {volumeCredits} credits\n";
        return result;
    }
}
