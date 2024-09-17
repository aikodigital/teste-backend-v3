using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

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
                    if (perf.Audience > 30) {
                        thisAmount += 1000 * (perf.Audience - 30);
                    }
                    break;
                case "comedy":
                    if (perf.Audience > 20) {
                        thisAmount += 10000 + 500 * (perf.Audience - 20);
                    }
                    thisAmount += 300 * perf.Audience;
                    break;
                case "history":
                    // Valor para peça de tragédia
                    var tragedyAmount = thisAmount;
                    if (perf.Audience > 30)
                    {
                        tragedyAmount += 1000 * (perf.Audience - 30);
                    }

                    // Valor para peça de comédia
                    var comedyAmount = thisAmount + 300 * perf.Audience;
                    if (perf.Audience > 20)
                    {
                        comedyAmount += 10000 + 500 * (perf.Audience - 20);
                    }

                    // Soma dos valores de tragédia e comédia
                    thisAmount = tragedyAmount + comedyAmount;

                    break;
                default:
                    throw new Exception("unknown type: " + play.Type);
            }
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


    public string Xml(Invoice invoice, Dictionary<string, Play> plays)
    {
        decimal totalAmount = 0;
        var volumeCredits = 0;
        var result = string.Format("Statement for {0}\n", invoice.Customer);
        CultureInfo cultureInfo = new CultureInfo("en-US");

        var statementElement = new XElement("Statement",
          new XAttribute(XNamespace.Xmlns + "xsi", "http://www.w3.org/2001/XMLSchema-instance"),
          new XAttribute(XNamespace.Xmlns + "xsd", "http://www.w3.org/2001/XMLSchema"),
          new XElement("Customer", invoice.Customer),
          new XElement("Items")
      );


        foreach (var perf in invoice.Performances)
        {
            var credits = 0;
            var play = plays[perf.PlayId];
            var lines = play.Lines;
            if (lines < 1000) lines = 1000;
            if (lines > 4000) lines = 4000;
            decimal thisAmount = lines * 10;
            switch (play.Type)
            {
                case "tragedy":
                    if (perf.Audience > 30)
                    {
                        thisAmount += 1000 * (perf.Audience - 30);
                    }
                    break;
                case "comedy":
                    if (perf.Audience > 20)
                    {
                        thisAmount += 10000 + 500 * (perf.Audience - 20);
                    }
                    thisAmount += 300 * perf.Audience;
                    break;
                case "history":
                    // Valor para peça de tragédia
                    var tragedyAmount = thisAmount;
                    if (perf.Audience > 30)
                    {
                        tragedyAmount += 1000 * (perf.Audience - 30);
                    }

                    // Valor para peça de comédia
                    var comedyAmount = thisAmount + 300 * perf.Audience;
                    if (perf.Audience > 20)
                    {
                        comedyAmount += 10000 + 500 * (perf.Audience - 20);
                    }

                    // Soma dos valores de tragédia e comédia
                    thisAmount = tragedyAmount + comedyAmount;

                    break;
                default:
                    throw new Exception("unknown type: " + play.Type);
            }

            credits += Math.Max(perf.Audience - 30, 0);


            // add extra credit for every ten comedy attendees
            if ("comedy" == play.Type) credits += (int)Math.Floor((decimal)perf.Audience / 5);

            var itemElement = new XElement("Item",
                new XElement("AmountOwed", Convert.ToDecimal(thisAmount / 100).ToString("0.##", cultureInfo)),
                new XElement("EarnedCredits", credits),
                new XElement("Seats", perf.Audience)
            );
            volumeCredits += credits;
            statementElement.Element("Items").Add(itemElement);
            totalAmount += thisAmount;
        }

        statementElement.Add(new XElement("AmountOwed", Convert.ToDecimal(totalAmount / 100).ToString("0.##", cultureInfo)));
        statementElement.Add(new XElement("EarnedCredits", volumeCredits));

        var document = new XDocument(new XDeclaration("1.0", "utf-8",null), statementElement);
        return "<?xml version=\"1.0\" encoding=\"utf-8\"?>\n" + document.ToString();
    }





}
