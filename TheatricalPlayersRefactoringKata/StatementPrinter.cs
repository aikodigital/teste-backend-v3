using System;
using System.Collections.Generic;
using System.Globalization;
using TheatricalPlayersRefactoringKata.Calculators;

namespace TheatricalPlayersRefactoringKata;


public class StatementPrinter
{
    private readonly Dictionary<string, IPlay> _calculators;

    public StatementPrinter()
    {
        _calculators = new Dictionary<string, IPlay>
        {
            { "tragedy", new Tragedy() },
            { "comedy", new Comedy() },
            { "history", new History() }
        };
    }

    public string Print(Invoice invoice, Dictionary<string, Play> plays)
    {
        var totalAmount = 0.0;
        var volumeCredits = 0;
        var result = string.Format("Statement for {0}\n", invoice.Customer);
        CultureInfo cultureInfo = new CultureInfo("en-US");

        foreach (var perf in invoice.Performances)
        {
            var play = plays[perf.PlayId];
            var calculator = _calculators[play.Type];

            var thisAmount = calculator.CalculateAmount(play, perf);
            volumeCredits += calculator.CalculateVolumeCredits(perf);

            result += String.Format(cultureInfo, "  {0}: {1:C} ({2} seats)\n", play.Name, Convert.ToDecimal(thisAmount), perf.Audience);
            totalAmount += thisAmount;
        }

        result += String.Format(cultureInfo, "Amount owed is {0:C}\n", Convert.ToDecimal(totalAmount));
        result += String.Format("You earned {0} credits\n", volumeCredits);
        return result;
    }
}

