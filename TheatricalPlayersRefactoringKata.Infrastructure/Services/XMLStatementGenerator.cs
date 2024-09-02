using System.Globalization;
using System.Text;
using System.Xml.Linq;
using TheatricalPlayersRefactoringKata.Entities;
using TheatricalPlayersRefactoringKata.Interfaces;
using TheatricalPlayersRefactoringKata.Services;

namespace TheatricalPlayersRefactoringKata.Infrastructure.Services
{
    public class XMLStatementGenerator : IStatementGenerator
    {
        private PlayTypeCalculatorFactory _calculatorFactory = new PlayTypeCalculatorFactory();
        private CultureInfo _cultureInfo = CultureInfo.GetCultureInfo("en-US");

        public string GenerateStatement(Invoice invoice, Dictionary<string, Play> plays)
        {
            var items = invoice.Performances.Select(performance =>
            {
                var play = plays[performance.PlayId];
                var calculator = _calculatorFactory.GetCalculator(play.Type);
                var amount = calculator.CalculateAmount(play, performance);
                var credits = calculator.CalculateCredits(performance);

                return new XElement("Item",
                    new XElement("AmountOwed", FormatNumber(amount)),
                    new XElement("EarnedCredits", credits),
                    new XElement("Seats", performance.Audience)
                );
            });

            var XMLdocument = new XDocument(
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

            using (var memoryStream = new MemoryStream())
            {
                XMLdocument.Save(memoryStream);
                string utf8String = Encoding.UTF8.GetString(memoryStream.ToArray());
                return utf8String;
            }
        }

        private string FormatNumber(decimal number)
        {
            string format = (number % 1 == 0) ? "F0" : "F1";
            return number.ToString(format, _cultureInfo);
        }
    }
}
