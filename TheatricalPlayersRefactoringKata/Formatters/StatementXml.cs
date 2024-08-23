using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using TheatricalPlayersRefactoringKata.Calculators;
using TheatricalPlayersRefactorinKata.Interfaces;
namespace TheatricalPlayersRefactoringKata.Formatters
{
   internal class StatementXml : IStatementStrategy
    {
        private readonly PlayTypeChange playtype = new PlayTypeChange();

        public string StatementFormat(Invoice invoice, Dictionary<string, Play> plays)
        {
            var totalAmount = 0.00m;
            var volumeCredits = 0;
            var cultureInfo = new CultureInfo("en-US");
            var itemsElements = invoice.Performances.Select(perf => 
                {    
                    var play = plays[perf.PlayId];
                    var calculate = playtype.Change(play);
                    decimal amount = calculate.CalculateAmount(play, perf);
                    int credits = calculate.CalculateCredits(play, perf);
                    volumeCredits += credits;
                    totalAmount += amount;

                    return new XElement("Item",
                        new XElement("AmountOwed", amount.ToString("0.##",cultureInfo)),
                        new XElement("EarnedCredits", credits.ToString(cultureInfo)),
                        new XElement("Seats", perf.Audience)
                    );
                
                }).ToList();
                
            var doc = new XDocument(
                    new XDeclaration("1.0", "utf-8", null),
                    new XElement("Statement",
                        new XAttribute(XNamespace.Xmlns + "xsi", "http://www.w3.org/2001/XMLSchema-instance"),
                        new XAttribute(XNamespace.Xmlns + "xsd", "http://www.w3.org/2001/XMLSchema"),
                        new XElement("Customer", invoice.Customer),
                        new XElement("Items", itemsElements),
                        new XElement("AmountOwed", totalAmount.ToString("0.##", cultureInfo)),
                        new XElement("EarnedCredits", volumeCredits.ToString("0.##", cultureInfo))
                    )
                );
            string pathXml = Path.Combine(Path.GetTempPath(), "received.xml");
            using (var fileStream = new FileStream(pathXml, FileMode.Create, FileAccess.Write))
            using (var streamWriter = new StreamWriter(fileStream, Encoding.UTF8))
            {
                streamWriter.Write("\ufeff");
                doc.Save(streamWriter);
            }            
            return File.ReadAllText(pathXml);
        
        }
    }
}