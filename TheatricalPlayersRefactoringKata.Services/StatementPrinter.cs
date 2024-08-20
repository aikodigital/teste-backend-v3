using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml;
using System.Xml.Linq;
using TheatricalPlayersRefactoringKata.Enums;
using TheatricalPlayersRefactoringKata.Models;
using TheatricalPlayersRefactoringKata.Services.InvoicePrice;

namespace TheatricalPlayersRefactoringKata.Services;

public class StatementPrinter
{
    public string PrintText(Invoice invoice)
    {
        var totalAmount = 0;
        var volumeCredits = 0;
        var result = string.Format("Statement for {0}\n", invoice.Customer);
        CultureInfo cultureInfo = new CultureInfo("en-US");

        foreach(var perf in invoice.Performances) 
        {
            var thisAmount = PerformancePrice.Price(perf.Play.Lines, perf.Audience, perf.Play.Type);

            // add volume credits
            volumeCredits += PerformancePrice.Credits(perf.Audience, perf.Play.Type == PlayType.Comedy);

            // print line for this order
            result += String.Format(cultureInfo, "  {0}: {1:C} ({2} seats)\n", perf.Play.Name, Convert.ToDecimal(((double)thisAmount) / 100), perf.Audience);
            totalAmount += thisAmount;
        }
        result += String.Format(cultureInfo, "Amount owed is {0:C}\n", Convert.ToDecimal(((double)totalAmount) / 100));
        result += String.Format("You earned {0} credits\n", volumeCredits);
        return result;
    }

    public string PrintXml(Invoice invoice)
    {
        var totalAmount = 0;
        var volumeCredits = 0;
        CultureInfo cultureInfo = new CultureInfo("en-US");

        var document = new XmlDocument();

        var statement = document.CreateElement("Statement");
        statement.SetAttribute("xmlns:xsi", "http://www.w3.org/2001/XMLSchema-instance");
        statement.SetAttribute("xmlns:xsd", "http://www.w3.org/2001/XMLSchema");
        document.AppendChild(statement);

        var customer = document.CreateElement("Customer");
        customer.InnerText = invoice.Customer;
        statement.AppendChild(customer);  

        var items = document.CreateElement("Items");
        statement.AppendChild(items);  

        foreach (var perf in invoice.Performances)
        {
            var thisAmount = PerformancePrice.Price(perf.Play.Lines, perf.Audience, perf.Play.Type);
            var currentCredits = PerformancePrice.Credits(perf.Audience, perf.Play.Type == PlayType.Comedy);
            volumeCredits += currentCredits;

            var item = document.CreateElement("Item");

            var amount = document.CreateElement("AmountOwed");
            var thisAmountDecimal = Convert.ToDecimal(((double)thisAmount) / 100);
            amount.InnerText = thisAmountDecimal.ToString(cultureInfo);
            item.AppendChild(amount);

            var credits = document.CreateElement("EarnedCredits");
            credits.InnerText = currentCredits.ToString();
            item.AppendChild(credits);

            var seats = document.CreateElement("Seats");
            seats.InnerText = perf.Audience.ToString();
            item.AppendChild(seats);

            items.AppendChild(item);

            totalAmount += thisAmount;
        }

        var amountOwed = document.CreateElement("AmountOwed");
        var totalAmountDecimal = Convert.ToDecimal(((double)totalAmount) / 100);
        amountOwed.InnerText = totalAmountDecimal.ToString(cultureInfo);
        statement.AppendChild(amountOwed);  

        var earnedCredits = document.CreateElement("EarnedCredits");
        earnedCredits.InnerText = volumeCredits.ToString();
        statement.AppendChild(earnedCredits);  
        
        using(var reader = new XmlNodeReader(document))
        {
            var result = XDocument.Load(reader, LoadOptions.PreserveWhitespace);

            result.Declaration = new XDeclaration("1.0", "utf-8", null);

            return "\uFEFF" + result.Declaration + Environment.NewLine + result;
        }
    }

    //implementation for future print methods

}
