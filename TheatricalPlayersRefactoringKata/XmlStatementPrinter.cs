using System.Collections.Generic;
using System.Globalization;
using TheatricalPlayersRefactoringKata.Models;

namespace TheatricalPlayersRefactoringKata
{
    public class XmlStatementPrinter
    {
        private readonly ICalculatorFactory _calculatorFactory;

        public XmlStatementPrinter(ICalculatorFactory calculatorFactory)
        {
            _calculatorFactory = calculatorFactory;
        }

        public string Print(Invoice invoice, Dictionary<string, Play> plays)
        {
            var totalAmount = 0;
            var volumeCredits = 0;
            var result = $"<Statement>\n  <Customer>{invoice.Customer}</Customer>\n";
            CultureInfo cultureInfo = new CultureInfo("en-US");

            foreach (var perf in invoice.Performances)
            {
                var play = plays[perf.PlayId];
                var calculator = _calculatorFactory.GetCalculator(play.Type);
                var thisAmount = calculator.CalculateAmount(perf);

                volumeCredits += calculator.CalculateCredits(perf);

                result += $"  <Play>{play.Name}</Play>: {thisAmount / 100.0:C} ({perf.Audience} seats)\n";
                totalAmount += (int)thisAmount;
            }

            result += $"  <AmountOwed>{totalAmount / 100.0:C}</AmountOwed>\n";
            result += $"  <Credits>{volumeCredits}</Credits>\n";
            result += "</Statement>";
            return result;
        }
    }
}
