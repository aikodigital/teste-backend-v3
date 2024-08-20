using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TheatricalPlayersRefactoringKata.Entities;

namespace TheatricalPlayersRefactoringKata.Formatters
{
    public class ExtractXMLFormatter : IExtractFormatter
    {
        //Generate statement in format XML
        public string Formatter(Invoice invoice, Dictionary<string, Play> plays)
        {

            double totalAmount = 0;
            double volumeCredits = 0;

            var statementElement = new XElement("Statement",
                new XElement("Customer", invoice.Customer),
                new XElement("Item"));

            foreach (var perf in invoice.Performances)
            {
                var play = plays[perf.PlayId];
                //Calculate the total value of the audience
                double thisAmount = play.Calculate(perf.Audience);
                totalAmount += thisAmount;
                // Add volume credits
                double thisVolumeCredits = play.VolumeCredits(perf.Audience);
                volumeCredits += thisVolumeCredits;

                statementElement.Element("Item")?.Add(new XElement("Item",
                    new XElement("AmountOwed", thisAmount),
                    new XElement("EarnedCredits", thisVolumeCredits),
                    new XElement("Seats", perf.Audience)));
            }
            //Final result
            statementElement.Add(new XElement("AmountOwed", totalAmount));
            statementElement.Add(new XElement("VolumeCredits", volumeCredits));

            return statementElement.ToString();
        }
    }
}
