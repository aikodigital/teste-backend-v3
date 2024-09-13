using System.Globalization;
using System.Text;
using System.Xml;
using TheatricalPlayersRefactoringKata.Application.Calculator;
using TheatricalPlayersRefactoringKata.Entities;

namespace TheatricalPlayersRefactoringKata.Application.Printer
{
    public class XmlStatementPrinter : IStatementPrinter
    {
        public string Print(Invoice invoice, Dictionary<string, Play> plays)
        {
            var totalAmount = 0;
            var volumeCredits = 0;

            using MemoryStream memoryStream = new();
            XmlWriterSettings settings = new()
            {
                Indent = true,
                Encoding = Encoding.UTF8
            };

            using (XmlWriter writer = XmlWriter.Create(memoryStream, settings))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("Statement");
                writer.WriteAttributeString("xmlns", "xsi", null, "http://www.w3.org/2001/XMLSchema-instance");
                writer.WriteAttributeString("xmlns", "xsd", null, "http://www.w3.org/2001/XMLSchema");
                writer.WriteElementString("Customer", invoice.Customer);
                writer.WriteStartElement("Items");

                foreach (var perf in invoice.Performances)
                {
                    var play = plays[perf.PlayId];
                    var thisAmount = play.CalculateBaseAmount();

                    IPlayAmountCalculator calculator = PlayAmountCalculatorFactory.GetCalculator(play.Type);
                    thisAmount = calculator.CalculateAmount(perf, thisAmount);

                    var earnedCredits = calculator.CalculateEarnedCredits(perf.Audience);
                    volumeCredits += earnedCredits;

                    totalAmount += thisAmount;

                    writer.WriteStartElement("Item");
                    writer.WriteElementString("AmountOwed", Convert.ToDecimal(thisAmount / 100m).ToString(CultureInfo.InvariantCulture));
                    writer.WriteElementString("EarnedCredits", earnedCredits.ToString(CultureInfo.InvariantCulture));
                    writer.WriteElementString("Seats", perf.Audience.ToString());

                    writer.WriteEndElement();
                }

                writer.WriteEndElement();

                writer.WriteElementString("AmountOwed", Convert.ToDecimal(totalAmount / 100m).ToString(CultureInfo.InvariantCulture));
                writer.WriteElementString("EarnedCredits", volumeCredits.ToString(CultureInfo.InvariantCulture));

                writer.WriteEndElement();
                writer.WriteEndDocument();
            }

            byte[] xmlBytes = memoryStream.ToArray();

            return Encoding.UTF8.GetString(xmlBytes);
        }
    }
}
