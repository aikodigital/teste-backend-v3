using System.Collections.Generic;
using System.Globalization;
using TheatricalPlayersRefactoringKata.Factories;
using TheatricalPlayersRefactoringKata.Models;

namespace TheatricalPlayersRefactoringKata.OutputStrategies
{
    public class TxtStatementOutputStrategy : IStatementOutputStrategy
    {
        public string Format(Invoice invoice, Dictionary<string, Play> plays, decimal totalAmount, int totalCredits)
        {
            var result = string.Format("Statement for {0}\n", invoice.Customer);
            CultureInfo cultureInfo = new CultureInfo("en-US");

            foreach (var perf in invoice.Performances)
            {
                var play = plays[perf.PlayId];
                var lines = play.Lines < 1000 ? 1000 : play.Lines > 4000 ? 4000 : play.Lines;
                var strategy = PlayTypeStrategyFactory.GetStrategy(play.Type);
                var thisAmount = strategy.CalculateAmount(lines, perf.Audience);

                result += string.Format(cultureInfo, "  {0}: {1:C} ({2} seats)\n", play.Name, thisAmount, perf.Audience);
            }

            result += string.Format(cultureInfo, "Amount owed is {0:C}\n", totalAmount);
            result += string.Format("You earned {0} credits\n", totalCredits);

            return result;
        }
    }
}
