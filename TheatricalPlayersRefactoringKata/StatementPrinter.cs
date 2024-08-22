using System;
using System.Collections.Generic;
using System.Globalization;
using TheatricalPlayersRefactoringKata.Models;

namespace TheatricalPlayersRefactoringKata
{
    public class StatementPrinter
    {
        private readonly ICalculatorFactory _calculatorFactory;

        public StatementPrinter(ICalculatorFactory calculatorFactory)
        {
            _calculatorFactory = calculatorFactory;
        }

        public string Print(Invoice invoice, Dictionary<string, Play> plays)
        {
            var totalAmount = 0;
            var volumeCredits = 0;
            var result = $"Statement for {invoice.Customer}\n";
            CultureInfo cultureInfo = new CultureInfo("en-US");

            foreach (var perf in invoice.Performances)
            {
                var play = plays[perf.PlayId];
                var calculator = _calculatorFactory.GetCalculator(play.Type);
                var thisAmount = calculator.CalculateAmount(perf);

                volumeCredits += calculator.CalculateCredits(perf);

                result += String.Format(cultureInfo, "  {0}: {1:C} ({2} seats)\n", play.Name, thisAmount / 100.0, perf.Audience);
                totalAmount += (int)thisAmount;
            }

            result += String.Format(cultureInfo, "Amount owed is {0:C}\n", totalAmount / 100.0);
            result += String.Format("You earned {0} credits\n", volumeCredits);
            return result;
        }
    }
}
