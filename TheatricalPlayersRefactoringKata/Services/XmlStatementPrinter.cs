using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Xml;
using TheatricalPlayersRefactoringKata.Models;
using TheatricalPlayersRefactoringKata.Services;

namespace TheatricalPlayersRefactoringKata
{
    public class XmlStatementPrinter
    {
        private readonly Dictionary<string, IPerformanceCalculator> _calculators;

        public XmlStatementPrinter()
        {
            _calculators = new Dictionary<string, IPerformanceCalculator>
            {
                { "tragedy", new TragedyCalculator() },
                { "comedy", new ComedyCalculator() },
                { "historical", new HistoryCalculator() }
            };
        }

        public string Format(Invoice invoice, Dictionary<string, Play> plays)
        {
            var totalAmount = 0;
            var totalCredits = 0;
            var xmlBuilder = new StringBuilder();
            var xmlWriter = XmlWriter.Create(xmlBuilder, new XmlWriterSettings { Indent = true });

            xmlWriter.WriteStartDocument();
            xmlWriter.WriteStartElement("Statement");

            xmlWriter.WriteElementString("Customer", invoice.Customer);

            xmlWriter.WriteStartElement("Items");
            foreach (var perf in invoice.Performances)
            {
                var play = plays[perf.PlayId];
                var calculator = _calculators[play.Type];

                var thisAmount = calculator.CalculateAmount(perf, play);
                var volumeCredits = calculator.CalculateVolumeCredits(perf, play);

                totalAmount += thisAmount;
                totalCredits += volumeCredits;

                xmlWriter.WriteStartElement("Item");
                xmlWriter.WriteElementString("AmountOwed", (thisAmount / 100m).ToString(CultureInfo.InvariantCulture));
                xmlWriter.WriteElementString("EarnedCredits", volumeCredits.ToString());
                xmlWriter.WriteElementString("Seats", perf.Audience.ToString());
                xmlWriter.WriteEndElement();
            }
            xmlWriter.WriteEndElement();

            xmlWriter.WriteElementString("AmountOwed", (totalAmount / 100m).ToString(CultureInfo.InvariantCulture));
            xmlWriter.WriteElementString("EarnedCredits", totalCredits.ToString());

            xmlWriter.WriteEndElement();
            xmlWriter.WriteEndDocument();
            xmlWriter.Close();

            return xmlBuilder.ToString();
        }
    }
}
