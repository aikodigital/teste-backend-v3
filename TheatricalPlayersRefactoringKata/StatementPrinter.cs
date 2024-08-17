using System;
using System.Collections.Generic;
using System.Globalization;

namespace TheatricalPlayersRefactoringKata
{
    public class StatementPrinter
    {
        public string Print(Invoice invoice, Dictionary<string, Play> plays)
        {
            var totalAmount = 0;
            var volumeCredits = 0;
            var result = string.Format("Statement for {0}\n", invoice.Customer);
            CultureInfo cultureInfo = new("en-US");

            foreach (var perf in invoice.Performances)
            {
                var play = plays[perf.PlayId];
                IChargeStrategy strategy = GetChargeStrategy(play.Type);

                var thisAmount = strategy.CalculateBilling(perf, play);
                var thisCredits = strategy.CalculateCredits(perf);

                // Add volume credits
                volumeCredits += thisCredits;

                // Print line for this order
                result += String.Format(cultureInfo, "  {0}: {1:C} ({2} seats)\n", play.Name, Convert.ToDecimal(thisAmount / 100), perf.Audience);
                totalAmount += thisAmount;
            }

            result += String.Format(cultureInfo, "Amount owed is {0:C}\n", Convert.ToDecimal(totalAmount / 100));
            result += String.Format("You earned {0} credits\n", volumeCredits);
            return result;
        }

        private IChargeStrategy GetChargeStrategy(string playType)
        {
            return playType switch
            {
                "tragedy" => new TragedyCharge(),
                "comedy" => new CollectionChargy(),
                "historical" => new HistoryCharge(),
                _ => throw new Exception("unknown type: " + playType),
            };
        }
    }
}
