using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Xml.Linq;
using TheatricalPlayersRefactoringKata.Models;

namespace TheatricalPlayersRefactoringKata.Services;

public class StatementPrinter
{
    public string Print(Invoice invoice, Dictionary<string, IPlay> plays)
    {
        decimal totalAmount = 0;
        int volumeCredits = 0;
        var result = string.Format("Statement for {0}\n", invoice.Customer);
        CultureInfo cultureInfo = new CultureInfo("en-US");

        foreach (var perf in invoice.Performances)
        {
            var play = plays[perf.PlayId];
            var thisAmount = play.CalculateAmount(perf.Audience);
            volumeCredits += play.CalculateVolumeCredits(perf.Audience);

            result += string.Format(cultureInfo, "  {0}: {1:C} ({2} seats)\n", play.Name, thisAmount / 100, perf.Audience);
            totalAmount += thisAmount;
        }
        result += string.Format(cultureInfo, "Amount owed is {0:C}\n", totalAmount / 100);
        result += string.Format("You earned {0} credits\n", volumeCredits);
        return result;
    }

    public string PrintAsXml(Invoice invoice, Dictionary<string, IPlay> plays)
    {
        var doc = new XDocument(new XElement("Statement",
            new XElement("Customer", invoice.Customer),
            new XElement("Performances",
                invoice.Performances.Select(perf =>
                    new XElement("Performance",
                        new XElement("Play", plays[perf.PlayId].Name),
                        new XElement("Audience", perf.Audience),
                        new XElement("Amount", plays[perf.PlayId].CalculateAmount(perf.Audience) / 100),
                        new XElement("VolumeCredits", plays[perf.PlayId].CalculateVolumeCredits(perf.Audience))
                    )
                )
            ),
            new XElement("TotalAmount", invoice.Performances.Sum(p => plays[p.PlayId].CalculateAmount(p.Audience)) / 100),
            new XElement("TotalVolumeCredits", invoice.Performances.Sum(p => plays[p.PlayId].CalculateVolumeCredits(p.Audience)))
        ));
        return doc.ToString();
    }
}
