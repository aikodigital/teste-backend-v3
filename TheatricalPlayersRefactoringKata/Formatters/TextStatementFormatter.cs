using System.Collections.Generic;
using System.Globalization;
using System.Text;
using TheatricalPlayersRefactoringKata.Calculators;
using TheatricalPlayersRefactoringKata.Models;

namespace TheatricalPlayersRefactoringKata.Formatters
{
    internal class TextStatementFormatter : IStatementFormatter
    {
        private readonly CultureInfo _cultureInfo = CultureInfo.GetCultureInfo("en-US");
        private readonly PlayCalculatorFactory _calculatorFactory = new PlayCalculatorFactory();

        public string Format(Invoice invoice, Dictionary<string, Play> plays)
        {
            var result = new StringBuilder();
            result.AppendFormat("Statement for {0}\n", invoice.Customer);

            var totalAmount = 0m;
            var volumeCredits = 0;

            foreach (var perf in invoice.Performances)
            {
                var play = plays[perf.PlayName];
                var calculator = _calculatorFactory.GetCalculator(play.Type);
                var thisAmount = calculator.CalculateAmount(play, perf);
                var credits = calculator.CalculateVolumeCredits(play, perf);

                volumeCredits += credits;
                totalAmount += thisAmount;

                result.AppendFormat(_cultureInfo, "  {0}: {1:C2} ({2} seats)\n", play.Name, thisAmount, perf.Audience);
            }

            result.AppendFormat(_cultureInfo, "Amount owed is {0:C2}\n", totalAmount);
            result.AppendFormat("You earned {0} credits\n", volumeCredits);

            return result.ToString();
        }
    }
}
