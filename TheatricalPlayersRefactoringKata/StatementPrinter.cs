using System;
using System.Collections.Generic;
using System.Globalization;
using TheatricalPlayersRefactoringKata.Strategies;

namespace TheatricalPlayersRefactoringKata;

public class StatementPrinter
{
    private readonly Dictionary<string, IPlayStrategy> _strategies;

    public StatementPrinter()
    {
        _strategies = new Dictionary<string, IPlayStrategy>
        {
            { "tragedy", new TragedyPlayStrategy() },
            { "comedy", new ComedyPlayStrategy() },
            { "history", new HistoryPlayStrategy() }
        };
    }

    public string Print(Invoice invoice, Dictionary<string, Play> plays)
    {
        double totalAmount = 0;
        var volumeCredits = 0;
        var result = string.Format("Statement for {0}\n", invoice.Customer);
        CultureInfo cultureInfo = new CultureInfo("en-US");

        foreach (var perf in invoice.Performances)
        {
            var play = plays[perf.PlayId];
            var strategy = _strategies[play.Type];

            double thisAmount = strategy.CalculateAmount(perf.Audience, play.Lines);
            volumeCredits += strategy.CalculateCredits(perf.Audience);

            result += String.Format(cultureInfo, "  {0}: {1:C} ({2} seats)\n", play.Name, Convert.ToDecimal(thisAmount / 100), perf.Audience);
            totalAmount += thisAmount;
        }

        result += String.Format(cultureInfo, "Amount owed is {0:C}\n", Convert.ToDecimal(totalAmount / 100));
        result += String.Format("You earned {0} credits\n", volumeCredits);
        return result;
    }
}