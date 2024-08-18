using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml.Linq;

namespace TheatricalPlayersRefactoringKata;

public class StatementPrinter
{
    public string Print(Invoice invoice, Dictionary<string, Play> plays)
    {
        decimal totalAmount = 0;
        int volumeCredits = 0;
        var result = string.Format("Statement for {0}\n", invoice.Customer);
        CultureInfo cultureInfo = new CultureInfo("en-US");

        foreach (var perf in invoice.Performances)
        {
            var play = plays[perf.PlayId];
            decimal thisAmount = CalculatePlayAmount(play, perf);
            // print line for this order
            result += String.Format(cultureInfo, "  {0}: {1:C} ({2} seats)\n", play.Name, thisAmount / 100, perf.Audience);

            totalAmount += thisAmount;
        }
        volumeCredits = CalculateVolumeCredits(invoice, plays);

        result += String.Format(cultureInfo, "Amount owed is {0:C}\n", totalAmount / 100);
        result += String.Format("You earned {0} credits\n", volumeCredits);
        return result;
    }

    public XDocument GenerateXmlStatement(Invoice invoice, Dictionary<string, Play> plays)
    {
        decimal totalAmount = 0;
        int volumeCredits = 0;

        XNamespace xml = "http://www.w3.org/2001/XMLSchema-instance";
        XNamespace xsd = "http://www.w3.org/2001/XMLSchema";

        var items = new List<XElement>();

        foreach (var perf in invoice.Performances)
        {
            var play = plays[perf.PlayId];
            decimal thisAmount = CalculatePlayAmount(play, perf);
            totalAmount += thisAmount;
            int creditsForPlay = CalculateVolumeCreditsForPerformance(perf, play);

            items.Add(new XElement("Item",
                new XElement("AmountOwed", thisAmount / 100),
                new XElement("EarnedCredits", creditsForPlay),
                new XElement("Seats", perf.Audience)
            ));
        }
        XDocument doc = new XDocument(
            new XDeclaration("1.0", "utf-8", null),
            new XElement("Statement",
                new XAttribute(XNamespace.Xmlns + "xsi", xml),
                new XAttribute(XNamespace.Xmlns + "xsd", xsd),
                new XElement("Customer", invoice.Customer),
                new XElement("Items", items),
                new XElement("AmountOwed", totalAmount / 100),
                new XElement("EarnedCredits", volumeCredits = CalculateVolumeCredits(invoice, plays))
            )
        );
        return doc;
    }

    private decimal CalculatePlayAmount(Play play, Performance perf)
    {
        var lines = play.Lines;
        if (lines < 1000) lines = 1000;
        if (lines > 4000) lines = 4000;
        decimal thisAmount = lines * 10;
        switch (play.Type)
        {
            case "tragedy":
                thisAmount = CalculateTragedyAmount(perf, thisAmount);
                break;
            case "comedy":
                thisAmount = CalculateComedyAmount(perf, thisAmount);
                break;
            case "history":
                thisAmount = CalculateHistoryAmount(perf, thisAmount);
                break;
            default:
                throw new Exception("unknown type: " + play.Type);
        }
        return thisAmount;
    }

    private int CalculateVolumeCredits(Invoice invoice, Dictionary<string, Play> plays)
    {
        int volumeCredits = 0;

        foreach(var perf in invoice.Performances)
        {
            var play = plays[perf.PlayId];
            // add volume credits
            volumeCredits += Math.Max(perf.Audience - 30, 0);
            if ("comedy" == play.Type) volumeCredits += (int)Math.Floor((decimal)perf.Audience / 5);
        }
        return volumeCredits;
    }

    private int CalculateVolumeCreditsForPerformance(Performance perf, Play play)
    {
        int volumeCredits = Math.Max(perf.Audience - 30, 0);
        if ("comedy" == play.Type) volumeCredits += (int)Math.Floor((decimal)perf.Audience / 5);
        return volumeCredits;
    }

    private static decimal CalculateComedyAmount(Performance perf, decimal thisAmount)
    {
        if (perf.Audience > 20)
        {
            thisAmount += 10000 + 500 * (perf.Audience - 20);
        }
        thisAmount += 300 * perf.Audience;
        return thisAmount;
    }

    private static decimal CalculateTragedyAmount(Performance perf, decimal thisAmount)
    {
        if (perf.Audience > 30)
        {
            thisAmount += 1000 * (perf.Audience - 30);
        }

        return thisAmount;
    }

    private static decimal CalculateHistoryAmount(Performance perf, decimal thisAmount)
    {
        decimal tragedyAmount = CalculateTragedyAmount(perf, thisAmount);

        decimal comedyAmount = CalculateComedyAmount(perf, thisAmount);

        thisAmount = tragedyAmount + comedyAmount;
        return thisAmount;
    }

}
