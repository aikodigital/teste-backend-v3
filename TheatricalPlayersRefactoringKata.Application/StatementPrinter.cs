using System.Globalization;
using TheatricalPlayersRefactoringKata.Domain.Entities;
using TheatricalPlayersRefactoringKata.Domain.Enums;
using TheatricalPlayersRefactoringKata.Domain.Services.Calculators;

namespace TheatricalPlayersRefactoringKata.Application;

public class StatementPrinter
{
    public string Print(Invoice invoice, Dictionary<string, Play> plays)
    {
        var totalAmount = 0;
        var volumeCredits = 0;
        var result = string.Format("Statement for {0}\n", invoice.Customer);
        CultureInfo cultureInfo = new CultureInfo("en-US");

        foreach(var perf in invoice.Performances) 
        {
            var play = plays[perf.PlayId];
            var baseAmount = play.CalculateBaseAmount();
            switch (play.Genre) 
            {
                case Genre.Tragedy:
                    baseAmount = TragedyAmountCalculator.Calculate(perf, play, baseAmount);
                    break;
                case Genre.Comedy:
                    baseAmount = ComedyAmountCalculator.Calculate(perf, play, baseAmount);
                    break;
                default:
                    throw new Exception("unknown type: " + play.Genre);
            }
            // add volume credits
            volumeCredits += perf.CalculateVolumeCredits(play.Genre);

            result += String.Format(cultureInfo, "  {0}: {1:C} ({2} seats)\n", play.Name, Convert.ToDecimal(baseAmount / 100), perf.Audience);
            totalAmount += baseAmount;
        }
        result += String.Format(cultureInfo, "Amount owed is {0:C}\n", Convert.ToDecimal(totalAmount / 100));
        result += String.Format("You earned {0} credits\n", volumeCredits);
        return result;
    }
}
