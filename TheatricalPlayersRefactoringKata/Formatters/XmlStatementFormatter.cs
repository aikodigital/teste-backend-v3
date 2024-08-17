using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TheatricalPlayersRefactoringKata.Calculators;
using TheatricalPlayersRefactoringKata.Models;

namespace TheatricalPlayersRefactoringKata.Formatters
{
    internal class XmlStatementFormatter : IStatementFormatter
    {
        private readonly CultureInfo _cultureInfo = CultureInfo.GetCultureInfo("en-US");
        private readonly PlayCalculatorFactory _calculatorFactory = new PlayCalculatorFactory();

        public string Format(Invoice invoice, Dictionary<string, Play> plays)
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
            }).ToList();

            var doc = new XDocument(
                new XDeclaration("1.0", "utf-8", null),
                new XElement("Statement",
                    new XAttribute(XNamespace.Xmlns + "xsi", "http://www.w3.org/2001/XMLSchema-instance"),
                    new XAttribute(XNamespace.Xmlns + "xsd", "http://www.w3.org/2001/XMLSchema"),
                    new XElement("Customer", invoice.Customer),
                    new XElement("Items", items),
                    new XElement("AmountOwed", FormatNumber(items.Sum(i => decimal.Parse(i.Element("AmountOwed").Value, _cultureInfo)))),
                    new XElement("EarnedCredits", items.Sum(i => int.Parse(i.Element("EarnedCredits").Value)))
                )
            );

            string path = Path.Combine(Path.GetTempPath(), "temp.xml");
            using (var fileStream = new FileStream(path, FileMode.Create, FileAccess.Write))
            using (var streamWriter = new StreamWriter(fileStream, Encoding.UTF8))
            {
                streamWriter.Write("\ufeff");
                doc.Save(streamWriter);
            }
            return File.ReadAllText(path);
        }

        private string FormatNumber(decimal number)
        {
            return number % 1 == 0 ? number.ToString("F0", _cultureInfo) : number.ToString("F1", _cultureInfo);
        }
    }
}
