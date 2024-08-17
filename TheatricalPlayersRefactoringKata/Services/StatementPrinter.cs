using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using TheatricalPlayersRefactoringKata.Calculators;
using TheatricalPlayersRefactoringKata.Models;

namespace TheatricalPlayersRefactoringKata.Services
{
    public class StatementPrinter
    {
        private readonly CultureInfo _cultureInfo = CultureInfo.GetCultureInfo("en-US");
        private readonly PlayCalculatorFactory _calculatorFactory = new PlayCalculatorFactory();

        public string Print(Invoice invoice, Dictionary<string, Play> plays)
        {
            var result = new StringBuilder();
            result.AppendFormat("Statement for {0}\n", invoice.Customer);

            var totalAmount = 0m;
            var volumeCredits = 0;

            foreach (var perf in invoice.Performances)
            {
                var play = plays[perf.PlayId];
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
        public string PrintXml(Invoice invoice, Dictionary<string, Play> plays)
        {
            var items = invoice.Performances.Select(perf =>
            {
                var play = plays[perf.PlayId];
                var calculator = _calculatorFactory.GetCalculator(play.Type);
                var amount = calculator.CalculateAmount(play, perf);
                var credits = calculator.CalculateVolumeCredits(play, perf);

                return new XElement("Item",
                    new XElement("AmountOwed", FormatNumber(amount)),
                    new XElement("EarnedCredits", credits),
                    new XElement("Seats", perf.Audience)
                );
            });

            var doc = new XDocument(
                new XElement("Statement",
                    new XElement("Customer", invoice.Customer),
                    new XElement("Items", items),
                    new XElement("AmountOwed", FormatNumber(items.Sum(i => decimal.Parse(i.Element("AmountOwed").Value)))),
                    new XElement("EarnedCredits", items.Sum(i => int.Parse(i.Element("EarnedCredits").Value)))
                )
            );

            return doc.ToString();
        }

        private string FormatNumber(decimal number)
        {
            return number % 1 == 0 ? number.ToString("F0", _cultureInfo) : number.ToString("F1", _cultureInfo);
        }


    }
}
