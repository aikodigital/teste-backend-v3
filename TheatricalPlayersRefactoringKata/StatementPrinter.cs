using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Xml.Linq;

namespace TheatricalPlayersRefactoringKata;

public class StatementPrinter
{
    public string Print(Invoice invoice, Dictionary<string, Play> plays)
    {
        double totalAmount = 0;
        var volumeCredits = 0;
        var result = string.Format("Statement for {0}\n", invoice.Customer);
        CultureInfo cultureInfo = new CultureInfo("en-US");

        foreach(var perf in invoice.Performances) 
        {
            var play = plays[perf.PlayId];
            var lines = play.Lines;
            if (lines < 1000) lines = 1000;
            if (lines > 4000) lines = 4000;
            double thisAmount = lines * 10;

            //call factory and perform the right strategy
            thisAmount = play.Perform(thisAmount, perf.Audience);

            // add volume credits
            volumeCredits += Math.Max(perf.Audience - 30, 0);
            // add extra credit for every ten comedy attendees
            if ("comedy" == play.Type) volumeCredits += (int)Math.Floor((decimal)perf.Audience / 5);

            // print line for this order
            result += String.Format(cultureInfo, "  {0}: {1:C} ({2} seats)\n", play.Name, Convert.ToDecimal(thisAmount / 100), perf.Audience);
            totalAmount += thisAmount;
        }
        result += String.Format(cultureInfo, "Amount owed is {0:C}\n", Convert.ToDecimal(totalAmount / 100));
        result += String.Format("You earned {0} credits\n", volumeCredits);
        return result;
    }
    public XDocument PrintXML(Invoice invoice, Dictionary<string, Play> plays)
    {
        double totalAmount = 0;
        var volumeCredits = 0;
        CultureInfo cultureInfo = new CultureInfo("en-US");

        // create XML document
        var xDocument = new XDocument(
            new XDeclaration("1.0", "utf-8", null),
            new XElement("Statement",
                new XAttribute(XNamespace.Xmlns + "xsi", "http://www.w3.org/2001/XMLSchema-instance"),
                new XAttribute(XNamespace.Xmlns + "xsd", "http://www.w3.org/2001/XMLSchema"),
                new XElement("Customer", invoice.Customer),
                new XElement("Items",
                    from perf in invoice.Performances
                    let play = plays[perf.PlayId]
                    let lines = Math.Max(1000, Math.Min(4000, play.Lines))
                    let thisAmount = play.Perform(lines * 10, perf.Audience)
                    let thisCredits = Math.Max(perf.Audience - 30, 0) + 
                        (play.Type == "comedy" ? (int)Math.Floor((decimal)perf.Audience / 5) : 0)

                    select new XElement("Item",
                        new XElement("AmountOwed", thisAmount/100),
                        new XElement("EarnedCredits", thisCredits),
                        new XElement("Seats", perf.Audience)
                    )
                ),
                new XElement("AmountOwed", totalAmount),
                new XElement("EarnedCredits", volumeCredits)
            )
        );

        
        foreach (var perf in invoice.Performances)
        {
            var thisCredits = 0;
            var play = plays[perf.PlayId];
            // add volume credits
            thisCredits = Math.Max(perf.Audience - 30, 0);
            // add extra credit for every ten comedy attendees
            if (play.Type == "comedy")
                thisCredits += (int)Math.Floor((decimal)perf.Audience / 5);
            
            // add price to total
            var lines = Math.Max(1000, Math.Min(4000, play.Lines));
            totalAmount += play.Perform(lines * 10, perf.Audience);

            volumeCredits += thisCredits;
        }

        // update XML document with final amounts and credits
        var totalAmountElement = xDocument.Root.Element("AmountOwed");
        var volumeCreditsElement = xDocument.Root.Element("EarnedCredits");

        if (totalAmountElement != null)
            totalAmountElement.Value = string.Format(cultureInfo, "{0}", totalAmount / 100);

        if (volumeCreditsElement != null)
            volumeCreditsElement.Value = volumeCredits.ToString();

        return xDocument;
    }

}
