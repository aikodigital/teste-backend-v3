using System.Collections.Generic;
using System.Globalization;
using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using TheatricalPlayersRefactoringKata.Calculators;

namespace TheatricalPlayersRefactoringKata
{
    public class StatementPrinter
    {
        private readonly Dictionary<string, IPlay> _calculators;

        public StatementPrinter()
        {
            _calculators = new Dictionary<string, IPlay>
            {
                { "tragedy", new Tragedy() },
                { "comedy", new Comedy() },
                { "history", new History() }
            };
        }

        public string Print(Invoice invoice, Dictionary<string, Play> plays)
        {
            var totalAmount = 0.0;
            var volumeCredits = 0;
            var result = string.Format("Statement for {0}\n", invoice.Customer);
            CultureInfo cultureInfo = new CultureInfo("en-US");

            foreach (var perf in invoice.Performances)
            {
                var play = plays[perf.PlayId];
                var calculator = _calculators[play.Type];

                var thisAmount = calculator.CalculateAmount(play, perf);
                volumeCredits += calculator.CalculateVolumeCredits(perf);

                result += String.Format(cultureInfo, "  {0}: {1:C} ({2} seats)\n", play.Name, Convert.ToDecimal(thisAmount), perf.Audience);
                totalAmount += thisAmount;
            }

            result += String.Format(cultureInfo, "Amount owed is {0:C}\n", Convert.ToDecimal(totalAmount));
            result += String.Format("You earned {0} credits\n", volumeCredits);
            return result;
        }

        public string PrintXml(Invoice invoice, Dictionary<string, Play> plays)
        {
            var items = invoice.Performances.Select(perf =>
            {
                var play = plays[perf.PlayId];
                var calculator = _calculators[play.Type];
                var amountOwed = calculator.CalculateAmount(play, perf);
                var earnedCredits = calculator.CalculateVolumeCredits(perf);

                return new XElement("Item",
                    new XElement("AmountOwed", FormatAmount(amountOwed)),
                    new XElement("EarnedCredits", earnedCredits),
                    new XElement("Seats", perf.Audience)
                );
            }).ToList();

            var totalAmount = invoice.Performances.Sum(perf =>
            {
                var play = plays[perf.PlayId];
                var calculator = _calculators[play.Type];
                return calculator.CalculateAmount(play, perf);
            });

            var totalCredits = invoice.Performances.Sum(perf =>
            {
                var play = plays[perf.PlayId];
                var calculator = _calculators[play.Type];
                return calculator.CalculateVolumeCredits(perf);
            });

            var doc = new XDocument(
                new XElement("Statement",
                    new XAttribute(XNamespace.Xmlns + "xsi", "http://www.w3.org/2001/XMLSchema-instance"),
                    new XAttribute(XNamespace.Xmlns + "xsd", "http://www.w3.org/2001/XMLSchema"),
                    new XElement("Customer", invoice.Customer),
                    new XElement("Items", items),
                    new XElement("AmountOwed", FormatAmount(totalAmount)),
                    new XElement("EarnedCredits", totalCredits)
                )
            );

            return doc.ToString();
        }

        public void SaveXmlToFile(Invoice invoice, Dictionary<string, Play> plays, string filePath) 
        {
            // Generate the XML content from the invoice and play details using the PrintXml method
            var xmlContent = PrintXml(invoice, plays);

            // Write the XML content to the file specified by filePath
            // The File.WriteAllText method creates the file if it does not exist, or overwrites it if it does
            File.WriteAllText(filePath, xmlContent);
        }

        private string FormatAmount(double amount)
        {
            return amount % 1 == 0 ? ((int)amount).ToString() : amount.ToString("0.0", CultureInfo.InvariantCulture);
        }
    }
}
