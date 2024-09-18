using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Xml;
using System.Xml.Linq;

namespace TheatricalPlayersRefactoringKata;

public class StatementPrinter
{
    private string ResultDirectory;

    public StatementPrinter() 
    {
        ResultDirectory = Environment.CurrentDirectory.Split("\\bin")[0];
    }

    public static int CalculateAmount(string type, int baseValue, int audience)
    {
        switch (type)
        {
            case "tragedy":
                return baseValue + 1000 * Math.Max(audience - 30, 0);
            case "comedy":
                return baseValue + 500 * Math.Max(audience - 20, 0) + 300 * audience + 10000 * (int)Math.Min(Math.Floor((double)(audience / 21)), 1);
            case "history":
                return 2 * baseValue + 1000 * Math.Max(audience - 30, 0) + 500 * Math.Max(audience - 20, 0) + 300 * audience + 10000 * (int)Math.Min(Math.Floor((double)(audience / 21)), 1);
            default:
                return 0;
        }
    }

    public static int CalculateCredits(string type, int baseValue, int audience)
    {
        switch (type)
        {
            case "comedy":
                return baseValue + (int)Math.Floor((decimal)audience / 5);
            default:
                return baseValue;
        }
    }

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
            // calculate type credits and amount
            thisAmount = CalculateAmount(play.Type, thisAmount, perf.Audience);
            volumeCredits = CalculateCredits(play.Type, volumeCredits, perf.Audience);
            // add volume credits
            volumeCredits += Math.Max(perf.Audience - 30, 0);

            // print line for this order
            result += String.Format(cultureInfo, "  {0}: {1:C} ({2} seats)\n", play.Name, Convert.ToDecimal(thisAmount) / 100, perf.Audience);
            totalAmount += thisAmount;
        }
        result += String.Format(cultureInfo, "Amount owed is {0:C}\n", Convert.ToDecimal(totalAmount) / 100);
        result += String.Format("You earned {0} credits\n", volumeCredits);
        return result;
    }

    public XmlDocument GenerateXML(Invoice invoice, Dictionary<string, Play> plays)
    {
        var totalAmount = 0;
        var totalCredits = 0;
        XmlDocument statementFile = new XmlDocument();
        statementFile.InsertBefore(statementFile.CreateXmlDeclaration("1.0", "utf-8", null), statementFile.DocumentElement);

        XmlElement customerElement = statementFile.CreateElement("Customer");
        XmlElement itemsElement = statementFile.CreateElement("Items");
        XmlElement amountOwedElement = statementFile.CreateElement("AmountOwed");
        XmlElement earnedCreditsElement = statementFile.CreateElement("EarnedCredits");

        customerElement.InnerText = invoice.Customer;
        CultureInfo cultureInfo = new CultureInfo("en-US");

        foreach (var perf in invoice.Performances)
        {
            XmlElement itemElement = statementFile.CreateElement("Item");
            XmlElement itemAmountOwedElement = statementFile.CreateElement("AmountOwed");
            XmlElement itemEarnedCreditsElement = statementFile.CreateElement("EarnedCredits");
            XmlElement itemSeatsElement = statementFile.CreateElement("Seats");
            var play = plays[perf.PlayId];
            var lines = play.Lines;
            if (lines < 1000) lines = 1000;
            if (lines > 4000) lines = 4000;
            var thisAmount = lines * 10;
            // calculate type credits and amount
            thisAmount = CalculateAmount(play.Type, thisAmount, perf.Audience);
            var thisCredits = CalculateCredits(play.Type, 0, perf.Audience);
            // add volume credits
            thisCredits += Math.Max(perf.Audience - 30, 0);

            // print line for this order
            itemAmountOwedElement.InnerText = (Convert.ToDecimal(thisAmount) / 100).ToString(cultureInfo);
            itemEarnedCreditsElement.InnerText = thisCredits.ToString();
            itemSeatsElement.InnerText = perf.Audience.ToString();
            totalAmount += thisAmount;
            totalCredits += thisCredits;

            itemElement.AppendChild(itemAmountOwedElement);
            itemElement.AppendChild(itemEarnedCreditsElement);
            itemElement.AppendChild(itemSeatsElement);
            itemsElement.AppendChild(itemElement);
        }
        amountOwedElement.InnerText = (Convert.ToDecimal(totalAmount) / 100).ToString(cultureInfo);
        earnedCreditsElement.InnerText = totalCredits.ToString();
        XmlElement statementElement = statementFile.CreateElement("Statement");
        statementElement.SetAttribute("xmlns:xsi", "http://www.w3.org/2001/XMLSchema-instance");
        statementElement.SetAttribute("xmlns:xsd", "http://www.w3.org/2001/XMLSchema");

        statementElement.AppendChild(customerElement);
        statementElement.AppendChild(itemsElement);
        statementElement.AppendChild(amountOwedElement);
        statementElement.AppendChild(earnedCreditsElement);
        statementFile.AppendChild(statementElement);

        statementFile.Save(String.Format("{0}\\statement-{1}.xml", ResultDirectory, invoice.Customer));

        return statementFile;
    }
}
