using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Xml.Linq;

namespace TheatricalPlayersRefactoringKata;

public class StatementPrinter
{
    public string Print(Invoice invoice, Dictionary<string, Play> plays)
    {
        decimal totalAmount = 0;
        var volumeCredits = 0;
        var result = string.Format("Statement for {0}\n", invoice.Customer);
        CultureInfo cultureInfo = new CultureInfo("en-US");

        foreach(var perf in invoice.Performances) 
        {
            var play = plays[perf.PlayId];
            var lines = play.Lines;
            if (lines < 1000) lines = 1000;
            if (lines > 4000) lines = 4000;
            decimal thisAmount = lines * 10;
            switch (play.Type) 
            {
                case "tragedy":
                    thisAmount += CalcTragedyValue(perf);
                    break;
                case "comedy":
                    thisAmount += CalcComedyValue(perf);
                    break;
                case "history":
                    thisAmount = CalcHistoryValue(perf, thisAmount);
                    break;
                default:
                    throw new Exception("unknown type: " + play.Type);
            }
            // add volume credits
            volumeCredits += Math.Max(perf.Audience - 30, 0);
            // add extra credit for every ten comedy attendees
            if ("comedy" == play.Type) volumeCredits += (int)Math.Floor((decimal)perf.Audience / 5);

            // print line for this order
            result += String.Format(cultureInfo, "  {0}: {1:C} ({2} seats)\n", play.Name, thisAmount / 100, perf.Audience);

            totalAmount += thisAmount;
        }
        result += String.Format(cultureInfo, "Amount owed is {0:C}\n", totalAmount / 100);
        result += String.Format("You earned {0} credits\n", volumeCredits);
        return result;
    }

    int CalcTragedyValue(Performance perf)
    {
        int value = 0; 
        if (perf.Audience > 30)
        {
            value += 1000 * (perf.Audience - 30);
        }
        return value;
    }

    int CalcComedyValue(Performance perf)
    {
        int value = 0; 
        if (perf.Audience > 20)
        {
            value += 10000 + 500 * (perf.Audience - 20);
        }
        value += 300 * perf.Audience;
        return value;
    }

    decimal CalcHistoryValue(Performance perf, decimal performanceAmount)
    {
        decimal historyAmount = CalcTragedyValue(perf) + CalcComedyValue(perf) + performanceAmount;
        decimal adjustmentFactor = perf.Audience > 30 ? 1.39712m : 1.8432m;
        decimal adjustedAmount = (int)Math.Ceiling(historyAmount * adjustmentFactor);

        return adjustedAmount;
    }

    public string PrintXml(Invoice invoice, Dictionary<string, Play> plays)
    {
        var totalAmount = 0;
        var volumeCredits = 0;
        var totalCredits = 0;

        XNamespace xsi = "http://www.w3.org/2001/XMLSchema-instance";
        XNamespace xsd = "http://www.w3.org/2001/XMLSchema";

        XDocument doc = new XDocument(
            new XDeclaration("1.0", "utf-8", null),
            new XElement("Statement",
                new XAttribute(XNamespace.Xmlns + "xsi", xsi),
                new XAttribute(XNamespace.Xmlns + "xsd", xsd),
                new XElement("Customer", invoice.Customer),
                new XElement("Items")
            )
        );

        XElement itemsElement = doc.Root.Element("Items");

        foreach (var perf in invoice.Performances)
        {
            var play = plays[perf.PlayId];
            var lines = play.Lines;
            if (lines < 1000) lines = 1000;
            if (lines > 4000) lines = 4000;
            decimal thisAmount = lines * 10;

            switch (play.Type)
            {
                case "tragedy":
                    thisAmount += CalcTragedyValue(perf);
                    break;
                case "comedy":
                    thisAmount += CalcComedyValue(perf);
                    break;
                case "history":
                    thisAmount = CalcHistoryValue(perf, thisAmount);
                    break;
                default:
                    throw new Exception("unknown type: " + play.Type);
            }

            volumeCredits += Math.Max(perf.Audience - 30, 0);

            if ("comedy" == play.Type) volumeCredits += (int)Math.Floor((decimal)perf.Audience / 5);

            totalAmount += (int)thisAmount;
            totalCredits += volumeCredits;

            itemsElement.Add(
                new XElement("Item",
                    new XElement("AmountOwed", thisAmount / 100),
                    new XElement("EarnedCredits", volumeCredits),
                    new XElement("Seats", perf.Audience)
                )
            );

            volumeCredits = 0;
        }

        doc.Root.Add(
            new XElement("AmountOwed", (decimal)totalAmount / 100),
            new XElement("EarnedCredits", totalCredits)
        );

        using (var writer = new Utf8StringWriter())
        {
            doc.Save(writer);
            return writer.ToString();
        }
    }

    public class Utf8StringWriter : StringWriter
    {
        public override Encoding Encoding => Encoding.UTF8;
    }
}
