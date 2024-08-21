using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace TheatricalPlayersRefactoringKata
{
    public class XmlStatementPrinter
    {
        private readonly Dictionary<string, IPlayCategory> _playCategories;

        public XmlStatementPrinter(Dictionary<string, IPlayCategory> playCategories)
        {
            _playCategories = playCategories;
        }

        public string PrintXml(Invoice invoice, Dictionary<string, Play> plays)
        {
            var xmlSettings = new XmlWriterSettings
            {
                Indent = true,
                IndentChars = "  ",
                NewLineChars = "\r\n",
                NewLineHandling = NewLineHandling.Replace,
                Encoding = System.Text.Encoding.UTF8
            };

            var stringBuilder = new StringBuilder();

            using (var xmlWriter = XmlWriter.Create(stringBuilder, xmlSettings))
            {
                xmlWriter.WriteStartDocument();
                xmlWriter.WriteStartElement("Statement");

                xmlWriter.WriteElementString("Customer", invoice.Customer);

                xmlWriter.WriteStartElement("Items");

                decimal totalAmount = 0;
                int totalCredits = 0;

                foreach (var performance in invoice.Performances)
                {
                    var play = plays[performance.PlayId];
                    var category = _playCategories[play.Category];

                    decimal thisAmount = category.CalculateAmount(performance.Audience, play.Lines);
                    int thisCredits = category.CalculateCredits(performance.Audience);

                    xmlWriter.WriteStartElement("Item");
                    xmlWriter.WriteElementString("AmountOwed", thisAmount.ToString("0.##"));
                    xmlWriter.WriteElementString("EarnedCredits", thisCredits.ToString());
                    xmlWriter.WriteElementString("Seats", performance.Audience.ToString());
                    xmlWriter.WriteEndElement();

                    totalAmount += thisAmount;
                    totalCredits += thisCredits;
                }

                xmlWriter.WriteEndElement();

                xmlWriter.WriteElementString("AmountOwed", totalAmount.ToString("0.##"));
                xmlWriter.WriteElementString("EarnedCredits", totalCredits.ToString());

                xmlWriter.WriteEndElement();
                xmlWriter.WriteEndDocument();
            }

            return stringBuilder.ToString();
        }
    }
}
