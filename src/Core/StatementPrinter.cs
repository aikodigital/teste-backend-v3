using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml;

namespace TheatricalPlayersRefactoringKata;

public class StatementPrinter
{
    public string Print(Invoice invoice, Dictionary<string, Play> plays)
    {
        decimal thisAmount = 0;
        decimal totalAmount = 0;
        var volumeCredits = 0;
        var result = string.Format("Statement for {0}\n", invoice.Customer);
        CultureInfo cultureInfo = new CultureInfo("en-US");

        foreach(var perf in invoice.Performances) 
        {
            var play = plays[perf.PlayId];
            switch (play.Type) 
            {
                case "tragedy":
                    var tragedy = new Tragedy();
                    thisAmount = tragedy.CalculateAmount(play.Lines, perf.Audience);
                    break;
                case "comedy":
                    var comedy = new Comedy();
                    thisAmount = comedy.CalculateAmount(play.Lines, perf.Audience);

                    // add extra credit for every ten comedy attendees
                    volumeCredits += comedy.CalculateExtraCredits(perf.Audience);
                    break;
                case "history":
                    var history = new History();
                    thisAmount = history.CalculateAmount(play.Lines, perf.Audience);
                    break;
                default:
                    throw new Exception("unknown type: " + play.Type);
            }
            // add volume credits
            volumeCredits += Math.Max(perf.Audience - 30, 0);

            // print line for this order
            result += String.Format(cultureInfo, "  {0}: {1:C} ({2} seats)\n", play.Name, Convert.ToDecimal(thisAmount / 100), perf.Audience);
            totalAmount += thisAmount;
        }
        result += String.Format(cultureInfo, "Amount owed is {0:C}\n", Convert.ToDecimal(totalAmount / 100));
        result += String.Format("You earned {0} credits\n", volumeCredits);
        return result;
    }

    public string PrintXml(Invoice invoice, Dictionary<string, Play> plays)
    {
        var xmlDoc = new XmlDocument();

        var xmlDeclaration = xmlDoc.CreateXmlDeclaration("1.0", "utf-8", null);
        var statementElement = xmlDoc.CreateElement("Statement");
        var customerElement = xmlDoc.CreateElement("Customer");
        var itemsElement = xmlDoc.CreateElement("Items");

        xmlDoc.AppendChild(xmlDeclaration);

        statementElement.SetAttribute("xmlns:xsi", "http://www.w3.org/2001/XMLSchema-instance");
        statementElement.SetAttribute("xmlns:xsd", "http://www.w3.org/2001/XMLSchema");
        xmlDoc.AppendChild(statementElement);

        customerElement.InnerText = invoice.Customer;
        statementElement.AppendChild(customerElement);
        
        statementElement.AppendChild(itemsElement);

        decimal thisAmount = 0;
        decimal totalAmount = 0;
        var totalCredits = 0;

        foreach (var perf in invoice.Performances)
        {
            var play = plays[perf.PlayId];
            var itemCredits = 0;

            switch (play.Type)
            {
                case "tragedy":
                    var tragedy = new Tragedy();
                    thisAmount = tragedy.CalculateAmount(play.Lines, perf.Audience);
                    break;
                case "comedy":
                    var comedy = new Comedy();
                    thisAmount = comedy.CalculateAmount(play.Lines, perf.Audience);

                    // add extra credit for every ten comedy attendees
                    totalCredits += comedy.CalculateExtraCredits(perf.Audience);
                    itemCredits += comedy.CalculateExtraCredits(perf.Audience);
                    break;
                case "history":
                    var history = new History();
                    thisAmount = history.CalculateAmount(play.Lines, perf.Audience);
                    break;
                default:
                    throw new Exception("unknown type: " + play.Type);
            }
            // add volume credits
            totalCredits += Math.Max(perf.Audience - 30, 0);
            itemCredits += Math.Max(perf.Audience - 30, 0);

            totalAmount += thisAmount;

            var itemElement = xmlDoc.CreateElement("Item");
            var amountOwedElement = xmlDoc.CreateElement("AmountOwed");
            var earnedCreditsElement = xmlDoc.CreateElement("EarnedCredits");
            var seatsElement = xmlDoc.CreateElement("Seats");

            itemsElement.AppendChild(itemElement);

            amountOwedElement.InnerText = Convert.ToDecimal(thisAmount / 100).ToString("F1", CultureInfo.CreateSpecificCulture("en-US"));
            itemElement.AppendChild(amountOwedElement);

            earnedCreditsElement.InnerText = itemCredits.ToString();
            itemElement.AppendChild(earnedCreditsElement);

            seatsElement.InnerText = perf.Audience.ToString();
            itemElement.AppendChild(seatsElement);
        }

        var totalAmountElement = xmlDoc.CreateElement("AmountOwed");
        var totalCreditsElement = xmlDoc.CreateElement("EarnedCredits");

        totalAmountElement.InnerText = Convert.ToDecimal(totalAmount / 100).ToString("F1", CultureInfo.CreateSpecificCulture("en-US"));
        statementElement.AppendChild(totalAmountElement);

        totalCreditsElement.InnerText = totalCredits.ToString();
        statementElement.AppendChild(totalCreditsElement);

        return xmlDoc.OuterXml;
    }
}
