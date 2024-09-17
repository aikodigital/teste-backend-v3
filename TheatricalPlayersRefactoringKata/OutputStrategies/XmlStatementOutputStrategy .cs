using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using TheatricalPlayersRefactoringKata.Factories;
using TheatricalPlayersRefactoringKata.Models;

namespace TheatricalPlayersRefactoringKata.OutputStrategies
{
    public class XmlStatementOutputStrategy : IStatementOutputStrategy
    {
        public string Format(Invoice invoice, Dictionary<string, Play> plays, decimal totalAmount, int totalCredits)
        {
            var doc = new XDocument(
                new XDeclaration("1.0", "utf-8", ""),
                new XElement("Statement",
                    new XAttribute(XNamespace.Xmlns + "xsi", "http://www.w3.org/2001/XMLSchema-instance"),
                    new XAttribute(XNamespace.Xmlns + "xsd", "http://www.w3.org/2001/XMLSchema"),
                    new XElement("Customer", invoice.Customer),
                    new XElement("Items",
                        from perf in invoice.Performances
                        let play = plays[perf.PlayId]
                        let lines = Math.Clamp(play.Lines, 1000, 4000)
                        let strategy = PlayTypeStrategyFactory.GetStrategy(play.Type)
                        let amountOwed = strategy.CalculateAmount(lines, perf.Audience)
                        let earnedCredits = strategy.CalculateVolumeCredits(perf.Audience)
                        select new XElement("Item",
                            new XElement("AmountOwed", amountOwed),
                            new XElement("EarnedCredits", earnedCredits),
                            new XElement("Seats", perf.Audience)
                        )
                    ),
                    new XElement("AmountOwed", totalAmount),
                    new XElement("EarnedCredits", totalCredits)
                )
            );

            var settings = new XmlWriterSettings
            {
                Encoding = new UTF8Encoding(false), // false to omit BOM
                Indent = true,
                NewLineChars = "\r\n" // Ensures consistent newlines
            };

            using (var memoryStream = new MemoryStream())
            using (var xmlWriter = XmlWriter.Create(memoryStream, settings))
            {
                doc.WriteTo(xmlWriter);
                xmlWriter.Flush();
                memoryStream.Position = 0;

                using (var reader = new StreamReader(memoryStream, Encoding.UTF8))
                {
                    var xmlContent = reader.ReadToEnd().TrimStart(); // Trim leading spaces
                    return xmlContent;
                }
            }
        }
    }
}