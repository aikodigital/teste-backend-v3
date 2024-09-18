using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Calculators;
using TheatricalPlayersRefactoringKata.Calculatros;
using TheatricalPlayersRefactoringKata.Models;

namespace TheatricalPlayersRefactoringKata.Formatters
{
    public class TextFormatter
    {
        public string GenerateText(Invoice invoice, Dictionary<string, Play> plays)
        {
            var totalAmount = 0;
            var totalVolumeCredits = 0;
            var result = $"Statement for {invoice.Customer}\n";
            var cultureInfo = new CultureInfo("en-US");

            foreach (var perf in invoice.Performances)
            {
                var play = plays[perf.PlayId];
                var thisAmount = new PlayAmountCalculator().Calculate(perf, play);
                var volumeCredits = new VolumeCreditsCalculator().Calculate(perf, play);
                totalVolumeCredits += volumeCredits;

                result += string.Format(cultureInfo, "  {0}: {1:C} ({2} seats)\n", play.Name, Convert.ToDecimal(thisAmount / 100M), perf.Audience);
                totalAmount += thisAmount;
            }

            result += string.Format(cultureInfo, "Amount owed is {0:C}\n", Convert.ToDecimal(totalAmount / 100M));
            result += string.Format("You earned {0} credits\n", totalVolumeCredits);

            return result;
        }
    }
}
