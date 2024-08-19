using System;
using System.Xml.Linq;
using System.Globalization;
using System.Threading;
using TheatricalPlayersRefactoringKata.Enums;
using TheatricalPlayersRefactoringKata.Models;
using System.IO;

namespace TheatricalPlayersRefactoringKata;

public class StatementPrinter
{
    public string Print(Invoice invoice)
    {
        const int downLimitOfLines = 1000;
        const int upLimitOfLines = 4000;

        CultureInfo cultureInfo = new CultureInfo("en-US");
        Thread.CurrentThread.CurrentCulture = cultureInfo;
        Thread.CurrentThread.CurrentUICulture = cultureInfo;

        double totalAmount = 0;

        var result = string.Format($"Statement for {invoice.Customer.Name}\n");

        foreach (var performance in invoice.Performances)
        {
            double thisAmount = 0;

            if (performance.Play.Lines < downLimitOfLines)
                performance.Play.Lines = downLimitOfLines;

            if (performance.Play.Lines > upLimitOfLines)
                performance.Play.Lines = upLimitOfLines;

            switch (performance.Play.Type)
            {
                case (EPlayType.Tragedy):

                    thisAmount += TragedyCalc(thisAmount, performance.Play.Lines, performance.Audience, invoice);
                    break;

                case (EPlayType.Comedy):

                    thisAmount += ComedyCalc(thisAmount, performance.Play.Lines, performance.Audience, invoice);
                    break;

                case (EPlayType.History):

                    thisAmount += HistoryCalc(thisAmount, performance.Play.Lines, performance.Audience, invoice);
                    break;

                default:

                    throw new Exception($"Unknown type:  + {performance.Play.Type}");
            }

            result += String.Format(cultureInfo, $"  {performance.Play.Name}: {thisAmount:C} ({performance.Audience} seats)\n");

            totalAmount += thisAmount;
        }

        result += String.Format(cultureInfo, $"Amount owed is {totalAmount:C}\n");
        result += String.Format($"You earned {invoice.Customer.Credits} credits\n");

        return result;
    }

    public double AmountBaseTragedy(int lines)
    {
        return lines / (double)10;
    }
    public double AmountBaseComedy(int lines, int audiance)
    {
        double amount = ((lines / (double)10) + (3 * audiance));
        return amount;
    }
    public int AdditionalAmountTragedyOver30(int audiance) => 10 * (audiance - 30);
    public int AdditionalAmountComedyOver20(int audiance) => (100 + (5 * (audiance - 20)));
    public int AddCredits(int audiance) => (1 * (audiance - 30));
    public int AddBonusCredit(int audiance)
    {
        var calc = MathF.Floor(audiance / 5);
        var amount = (int)calc;
        return amount;
    }
    public double ComedyCalc(double amount, int lines, int audiance, Invoice invoice)
    {
        amount = AmountBaseComedy(lines, audiance);

        invoice.Customer.Credits += AddBonusCredit(audiance);
        invoice.Customer.PartialCredits += AddBonusCredit(audiance);

        if (audiance > 20)
            amount += AdditionalAmountComedyOver20(audiance);

        if (audiance > 30)
        {
            invoice.Customer.Credits += AddCredits(audiance);
            invoice.Customer.PartialCredits += AddCredits(audiance);
        }            

        return amount;
    }
    public double TragedyCalc(double amount, int lines, int audiance, Invoice invoice)
    {
        amount = AmountBaseTragedy(lines);

        if (audiance > 30)
        {
            amount += AdditionalAmountTragedyOver30(audiance);
            invoice.Customer.Credits += AddCredits(audiance);
            invoice.Customer.PartialCredits += AddCredits(audiance);
        }

        return amount;
    }
    public double HistoryCalc(double amount, int lines, int audiance, Invoice invoice)
    {
        amount = AmountBaseTragedy(lines);

        if (audiance > 30)
            amount += AdditionalAmountTragedyOver30(audiance);
            

        amount += AmountBaseComedy(lines, audiance);

        if (audiance > 20)
            amount += AdditionalAmountComedyOver20(audiance);

        if (audiance > 30)
        {
            invoice.Customer.Credits += AddCredits(audiance);
            invoice.Customer.PartialCredits += AddCredits(audiance);
        }            

        return amount;
    }

    public string Printer(Invoice invoice)
    {
        const int downLimitOfLines = 1000;
        const int upLimitOfLines = 4000;

        CultureInfo cultureInfo = new CultureInfo("en-US");
        Thread.CurrentThread.CurrentCulture = cultureInfo;
        Thread.CurrentThread.CurrentUICulture = cultureInfo;

        double totalAmount = 0;

        var document = new XDocument(new XDeclaration(version: "1.0", encoding: "utf-8", standalone: null), new XElement("Statement",
                    new XAttribute(XNamespace.Xmlns + "xsi", "http://www.w3.org/2001/XMLSchema-instance"),
                    new XAttribute(XNamespace.Xmlns + "xsd", "http://www.w3.org/2001/XMLSchema")));

        //var xmlStatement = new XElement("Statement",
        //            new XAttribute(XNamespace.Xmlns + "xsi", "http://www.w3.org/2001/XMLSchema-instance"),
        //            new XAttribute(XNamespace.Xmlns + "xsd", "http://www.w3.org/2001/XMLSchema"));


        var xmlCustomer = new XElement("Customer", invoice.Customer.Name);
        var xmlItems = new XElement("Items");

        document.Root.Add(xmlCustomer);
        document.Root.Add(xmlItems);

        foreach (var performance in invoice.Performances)
        {
            double thisAmount = 0;
            invoice.Customer.PartialCredits = 0;

            if (performance.Play.Lines < downLimitOfLines)
                performance.Play.Lines = downLimitOfLines;

            if (performance.Play.Lines > upLimitOfLines)
                performance.Play.Lines = upLimitOfLines;

            switch (performance.Play.Type)
            {
                case (EPlayType.Tragedy):

                    thisAmount += TragedyCalc(thisAmount, performance.Play.Lines, performance.Audience, invoice);
                    break;

                case (EPlayType.Comedy):

                    thisAmount += ComedyCalc(thisAmount, performance.Play.Lines, performance.Audience, invoice);
                    break;

                case (EPlayType.History):

                    thisAmount += HistoryCalc(thisAmount, performance.Play.Lines, performance.Audience, invoice);
                    break;

                default:

                    throw new Exception($"Unknown type:  + {performance.Play.Type}");
            }

            var xmlItem = new XElement("Item");

            xmlItem.Add(new XElement("AmountOwed", thisAmount));
            xmlItem.Add(new XElement("EarnedCredits", invoice.Customer.PartialCredits));
            xmlItem.Add(new XElement("Seats", performance.Audience));

            xmlItems.Add(xmlItem);

            totalAmount += thisAmount;
        }
        
        var xmlTotal = new XElement("AmountOwed", totalAmount);
        var xmlTotalCredits = new XElement("EarnedCredits", invoice.Customer.Credits);

        document.Root.Add(xmlTotal);
        document.Root.Add(xmlTotalCredits);

        string result = string.Format(cultureInfo,$"{document.Declaration.ToString()}");
        result += string.Format(document.ToString());
        //string path = @"C://dev/testes/test.xml";
        
        return result;
    }
}