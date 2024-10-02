using System;
using System.Collections.Generic;
using System.Globalization;
using TheatricalPlayersRefactoringKata.Calculators;
using TheatricalPlayersRefactoringKata.Core;

namespace TheatricalPlayersRefactoringKata
{
    public class StatementPrinter
    {
        private readonly Dictionary<string, IPlayTypeCalculator> _calculators;
        public StatementPrinter()
        {
            _calculators = new Dictionary<string, IPlayTypeCalculator>
            {
                { "comedy", new ComedyCalculator() },
                { "tragedy", new TragedyCalculator() },
                { "history", new HistoryCalculator() }
            };
        }
        public string Print(Invoice invoice, Dictionary<string, Play> plays)
        {
            decimal totalAmount = 0m;
            decimal volumeCredits = 0;
            var result = string.Format("Statement for {0}\n", invoice.Customer);
            CultureInfo cultureInfo = new CultureInfo("en-US");

            foreach (var perf in invoice.Performances)
            {
                var play = plays[perf.PlayId];
                var calculator = _calculators[play.Type];

                decimal thisAmount = calculator.CalculateAmount(perf, play);
                volumeCredits += calculator.CalculateVolumeCredits(perf);

                result += String.Format(cultureInfo, "  {0}: {1:C} ({2} seats)\n", play.Name, Convert.ToDecimal(thisAmount / 100), perf.Audience);
                totalAmount += thisAmount;
            }

            result += String.Format(cultureInfo, "Amount owed is {0:C}\n", Convert.ToDecimal(totalAmount / 100));
            result += String.Format("You earned {0} credits\n", volumeCredits);
            return result;
        }
    }
}
