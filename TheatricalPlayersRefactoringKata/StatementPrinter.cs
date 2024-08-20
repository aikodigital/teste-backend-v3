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
        var totalAmount = 0;
        var volumeCredits = 0;
        var result = string.Format("Statement for {0}\n", invoice.Customer);
        CultureInfo cultureInfo = new CultureInfo("en-US");

        foreach(var perf in invoice.Performances) 
        {
            var play = plays[perf.PlayId];
            var lines = play.Lines;
            if (lines < 1000) lines = 1000;
            if (lines > 4000) lines = 4000;
            var thisAmount = lines * 10;

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
    public string PrintXML(Invoice invoice, Dictionary<string, Play> plays)
    {
        var totalAmount = 0;
        var volumeCredits = 0;
        CultureInfo cultureInfo = new CultureInfo("en-US");

        // create XML document
        var xDocument = new XDocument(
            new XElement("Statement",
                new XElement("Customer", invoice.Customer),
                new XElement("Performances",
                    from perf in invoice.Performances
                    let play = plays[perf.PlayId]
                    let lines = Math.Max(1000, Math.Min(4000, play.Lines))
                    let thisAmount = play.Perform(lines * 10, perf.Audience)
                    let amountFormatted = $"{thisAmount / 100:C}"
                    let seatsFormatted = perf.Audience.ToString()

                    select new XElement("Performance",
                        new XElement("PlayName", play.Name),
                        new XElement("Amount", amountFormatted),
                        new XElement("Seats", seatsFormatted)
                    )
                ),
                new XElement("TotalAmount", $"{totalAmount / 100:C}"),
                new XElement("VolumeCredits", volumeCredits)
            )
        );

        
        foreach (var perf in invoice.Performances)
        {
            var play = plays[perf.PlayId];
            var lines = Math.Max(1000, Math.Min(4000, play.Lines));
            var thisAmount = lines * 10;
            
            // call factory and perform the right strategy
            thisAmount = play.Perform(thisAmount, perf.Audience);

            // add volume credits
            volumeCredits += Math.Max(perf.Audience - 30, 0);


            // add extra credit for every ten comedy attendees
            if (play.Type == "comedy")
                volumeCredits += (int)Math.Floor((decimal)perf.Audience / 5);

            totalAmount += thisAmount;
        }

        // update XML document with final amounts and credits
        var totalAmountElement = xDocument.Root.Element("TotalAmount");
        var volumeCreditsElement = xDocument.Root.Element("VolumeCredits");

        if (totalAmountElement != null)
            totalAmountElement.Value = $"{totalAmount / 100:C}";

        if (volumeCreditsElement != null)
            volumeCreditsElement.Value = volumeCredits.ToString();

        return xDocument.ToString();
    }
}
