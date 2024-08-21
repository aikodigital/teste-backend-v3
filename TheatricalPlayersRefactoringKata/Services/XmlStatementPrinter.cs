using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace TheatricalPlayersRefactoringKata
{
    public class XmlStatementPrinter
    {
        public string Print(Invoice invoice, Dictionary<string, Play> plays)
        {
            var items = new List<XElement>();
            decimal totalAmount = 0;
            int totalCredits = 0;

            foreach (var perf in invoice.Performances)
            {
                var play = plays[perf.PlayId];
                var calculator = CreatePlayCalculator(perf, play);
                var thisAmount = calculator.CalculateAmount();
                var thisCredits = calculator.CalculateVolumeCredits();

                items.Add(new XElement("Item",
                    new XElement("AmountOwed", FormatAmount(thisAmount)),
                    new XElement("EarnedCredits", thisCredits),
                    new XElement("Seats", perf.Audience)
                ));

                totalAmount += thisAmount;
                totalCredits += thisCredits;
            }

            var statement = new XElement("Statement",
                new XAttribute(XNamespace.Xmlns + "xsi", "http://www.w3.org/2001/XMLSchema-instance"),
                new XAttribute(XNamespace.Xmlns + "xsd", "http://www.w3.org/2001/XMLSchema"),
                new XElement("Customer", invoice.Customer),
                new XElement("Items", items),
                new XElement("AmountOwed", FormatAmount(totalAmount)),
                new XElement("EarnedCredits", totalCredits)
            );

            var settings = new XmlWriterSettings
            {
                Encoding = Encoding.UTF8, 
                Indent = true,
                IndentChars = "  ",
                OmitXmlDeclaration = false
            };

            using (var stream = new MemoryStream())
            using (var writer = new StreamWriter(stream, Encoding.UTF8))
            {
                using (var xmlWriter = XmlWriter.Create(writer, settings))
                {
                    statement.WriteTo(xmlWriter);
                    xmlWriter.Flush();
                }
                writer.Flush();
                return Encoding.UTF8.GetString(stream.ToArray());
            }
        }

        private string FormatAmount(decimal amount)
        {
            return amount % 1 == 0 ? amount.ToString("F0", CultureInfo.InvariantCulture) : amount.ToString("F1", CultureInfo.InvariantCulture);
        }

        private PlayCalculator CreatePlayCalculator(Performance perf, Play play)
        {
            switch (play.Type)
            {
                case "tragedy":
                    return new TragedyCalculator(perf, play);
                case "comedy":
                    return new ComedyCalculator(perf, play);
                case "history":
                    return new HistoryCalculator(perf, play, play);
                default:
                    throw new Exception("unknown type: " + play.Type);
            }
        }
    }
}
