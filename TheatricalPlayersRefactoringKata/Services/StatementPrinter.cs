using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using TheatricalPlayersRefactoringKata.Models;

namespace TheatricalPlayersRefactoringKata.Services;

public class StatementPrinter
{
    public string Print(Invoice invoice, Dictionary<string, Play> plays)
    {
        double totalAmount = 0;
        var volumeCredits = 0;

        var result = string.Format("Statement for {0}\n", invoice.Customer);

        CultureInfo cultureInfo = new CultureInfo("en-US");

        foreach (var perf in invoice.Performances)
        {
            var play = plays[perf.PlayId];

            double thisAmount = baseAmount(play);

            //call factory and perform the right strategy
            thisAmount = play.Perform(thisAmount, perf.Audience);

            
            // calculate credits
            volumeCredits += calculateCredits(perf, play);

            // print line for this order
            result += string.Format(cultureInfo, "  {0}: {1:C} ({2} seats)\n", play.Name, Convert.ToDecimal(thisAmount / 100), perf.Audience);
            
            totalAmount += thisAmount;
        }

        result += string.Format(cultureInfo, "Amount owed is {0:C}\n", Convert.ToDecimal(totalAmount / 100));
        result += string.Format("You earned {0} credits\n", volumeCredits);
       
        return result;
    }
    public string PrintXML(Invoice invoice, Dictionary<string, Play> plays)
    {
        double totalAmount = 0;
        var volumeCredits = 0;

        CultureInfo cultureInfo = new CultureInfo("en-US");

        var items = new XElement("Items");

        foreach (var perf in invoice.Performances)
        {

            var thisCredits = 0;
            var play = plays[perf.PlayId];

            double thisAmount = baseAmount(play);

            // call factory and perform the right strategy
            thisAmount = play.Perform(thisAmount, perf.Audience);

            // calculate credits
            thisCredits = calculateCredits(perf, play);

            // add item to XML
            items.Add(
                new XElement("Item",
                    new XElement("AmountOwed", thisAmount / 100),
                    new XElement("EarnedCredits", thisCredits),
                    new XElement("Seats", perf.Audience)
                )
            );

            // add price to total
            totalAmount += thisAmount;
            volumeCredits += thisCredits;
        }

        var statement = new XElement("Statement",
               new XAttribute(XNamespace.Xmlns + "xsi", "http://www.w3.org/2001/XMLSchema-instance"),
               new XAttribute(XNamespace.Xmlns + "xsd", "http://www.w3.org/2001/XMLSchema"),
               new XElement("Customer", invoice.Customer),
               items,
               new XElement("AmountOwed", string.Format(cultureInfo, "{0}", totalAmount / 100)),
               new XElement("EarnedCredits", volumeCredits)
           );

        // create XML document
        var xDocument = new XDocument(
            new XDeclaration("1.0", "utf-8", null),
            statement
        );

        using (var memoryStream = new MemoryStream())
        {
            using (var streamWriter = new StreamWriter(memoryStream, new UTF8Encoding(true)))
            {
                xDocument.Save(streamWriter);
            }

            return Encoding.UTF8.GetString(memoryStream.ToArray());
        }
    }

    internal int baseAmount(Play play)
    {
        var lines = play.Lines;
        if (lines < 1000) lines = 1000;
        if (lines > 4000) lines = 4000;
        return lines * 10;
    }
    internal int calculateCredits(Performance perf, Play play)
    {
        int thisCredits = 0;

        // add volume credits
        thisCredits = Math.Max(perf.Audience - 30, 0);

        // add extra credit for every ten comedy attendees
        if (play.Type == "comedy")
            thisCredits += (int)Math.Floor((decimal)perf.Audience / 5);

        return thisCredits;
    }

}