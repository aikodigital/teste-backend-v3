using System;
using System.Collections.Generic;
using System.Globalization;
using TheatricalPlayersRefactoringKata.Calculators;
using TheatricalPlayersRefactoringKata.Interfaces;

namespace TheatricalPlayersRefactoringKata.Formatters
{
   internal class StatementText : IStatementStrategy
    {
        public PlayTypeChange playtype = new PlayTypeChange();
        public string StatementFormat(Invoice invoice, Dictionary<string, Play> plays)
        {
            var totalAmount = 0.00m;
            var volumeCredits = 0;
            var result = string.Format("Statement for {0}\n", invoice.Customer);
            CultureInfo cultureInfo = new CultureInfo("en-US");

            foreach(var perf in invoice.Performances)
            {
                var play = plays[perf.PlayId];

                var calculate = playtype.Change(play.Type);

                var amount = calculate.CalculateAmount(play, perf);
                var credits = calculate.CalculateCredits(play, perf);
                volumeCredits += credits;
                result += String.Format(cultureInfo, "  {0}: {1:C} ({2} seats)\n", play.Name, amount, perf.Audience);
                totalAmount += amount;
            }

            result += String.Format(cultureInfo, "Amount owed is {0:C}\n", totalAmount);
            result += String.Format("You earned {0} credits\n", volumeCredits);

            return result;

        }
    }
}