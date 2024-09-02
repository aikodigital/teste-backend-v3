using System.Globalization;
using System.Text;
using TheatricalPlayersRefactoringKata.Entities;
using TheatricalPlayersRefactoringKata.Interfaces;
using TheatricalPlayersRefactoringKata.Services;

namespace TheatricalPlayersRefactoringKata.Infrastructure.Services
{
    public class TextStatementGenerator : IStatementGenerator
    {
        private PlayTypeCalculatorFactory _calculatorFactory = new PlayTypeCalculatorFactory();
        private CultureInfo _cultureInfo = CultureInfo.GetCultureInfo("en-US");

        public string GenerateStatement(Invoice invoice, Dictionary<string, Play> plays)
        {
            var result = new StringBuilder();
            result.AppendFormat("Statement for {0}\n", invoice.Customer);

            var totalAmount = 0m;
            var volumeCredits = 0;

            foreach (var performance in invoice.Performances)
            {
                var play = plays[performance.PlayId];
                var calculator = _calculatorFactory.GetCalculator(play.Type);
                var thisAmount = calculator.CalculateAmount(play, performance);
                volumeCredits += calculator.CalculateCredits(performance);

                result.AppendFormat(_cultureInfo, "  {0}: {1:C2} ({2} seats)\n", play.Name, thisAmount, performance.Audience);
                totalAmount += thisAmount;
            }

            result.AppendFormat(_cultureInfo, "Amount owed is {0:C2}\n", totalAmount);
            result.AppendFormat("You earned {0} credits\n", volumeCredits);

            return result.ToString();
        }
    }
}
