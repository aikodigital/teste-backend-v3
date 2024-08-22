using System;
using System.Xml.Linq;
using System.Globalization;
using System.Threading;
using TheatricalPlayersRefactoringKata.Enums;
using TheatricalPlayersRefactoringKata.Models;
using System.IO;
using System.Text;

namespace TheatricalPlayersRefactoringKata;

public class StatementPrinter
{
    public string PrinterTxt(Invoice invoice)
    {
        if (invoice is not null)
        {
            const int downLimitOfLines = 1000;
            const int upLimitOfLines = 4000;

            CultureInfo cultureInfo = new CultureInfo("en-US");
            Thread.CurrentThread.CurrentCulture = cultureInfo;
            Thread.CurrentThread.CurrentUICulture = cultureInfo;

            double totalAmount = 0;

            var result = string.Format($"Statement for {invoice.Customer.Name}\n");

            foreach (var performance in invoice.PerformanceList)
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
        else
        {
            throw new Exception("Invoice cannot be null");
        }

    }

    public string PrinterXml(Invoice invoice)
    {
        if (invoice is not null)
        {
            const int downLimitOfLines = 1000;
            const int upLimitOfLines = 4000;

            CultureInfo cultureInfo = new CultureInfo("en-US");
            Thread.CurrentThread.CurrentCulture = cultureInfo;
            Thread.CurrentThread.CurrentUICulture = cultureInfo;

            double totalAmount = 0;

            var document = new XDocument(new XDeclaration("1.0", UTF8Encoding.UTF8.ToString(), null), new XElement("Statement",
                        new XAttribute(XNamespace.Xmlns + "xsi", "http://www.w3.org/2001/XMLSchema-instance"),
                        new XAttribute(XNamespace.Xmlns + "xsd", "http://www.w3.org/2001/XMLSchema")));

            var xmlCustomer = new XElement("Customer", invoice.Customer.Name);
            var xmlItems = new XElement("Items");

            document.Root.Add(xmlCustomer);
            document.Root.Add(xmlItems);

            foreach (var performance in invoice.PerformanceList)
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

            string path = $"{Path.GetTempPath()}/teste.txt";

            string result;

            using (StreamWriter sw = new StreamWriter(path, false, new UTF8Encoding(false)))
            {
                document.Save(sw);
            }

            using (StreamReader sr = new StreamReader(path))
            {
                result = sr.ReadToEnd();
            }

            return result;
        }
        else
        {
            throw new Exception("Invoice cannot be null");
        }

    }

    public double AmountBaseTragedy(int lines) => lines > 0 ? lines / (double)10 : throw new Exception("Lines must be greater than 0");
    public double AmountBaseComedy(int lines, int audience) => lines > 0 && audience >= 0 ? ((lines / (double)10) + (3 * audience)) : throw new Exception("Lines must be greater than 0 and Audience must be equal or greater than 0");
    public int AdditionalAmountTragedyOver30(int audience) => audience > 30 ? (10 * (audience - 30)) : throw new Exception("Audience must be greater than 30 to this method");
    public int AdditionalAmountComedyOver20(int audience) => audience > 20 ? (100 + (5 * (audience - 20))) : throw new Exception("Audience must be greater than 20 to this method");
    public int AddCredits(int audience) => audience > 30 ? (1 * (audience - 30)) : throw new Exception("Audience must be greater than 30 to this method");
    public int AddBonusCredit(int audience)
    {
        if (audience >= 0)
        {
            var calc = MathF.Floor(audience / 5);
            var amount = (int)calc;
            return amount;
        }
        else
        {
            throw new Exception("Audience must be equal or greater than 0");
        }

    }
    public double ComedyCalc(double amount, int lines, int audience, Invoice invoice)
    {
        if (lines > 0 && audience >= 0 && invoice is not null)
        {
            amount = AmountBaseComedy(lines, audience);

            invoice.Customer.Credits += AddBonusCredit(audience);
            invoice.Customer.PartialCredits += AddBonusCredit(audience);

            if (audience > 20)
                amount += AdditionalAmountComedyOver20(audience);

            if (audience > 30)
            {
                invoice.Customer.Credits += AddCredits(audience);
                invoice.Customer.PartialCredits += AddCredits(audience);
            }

            return amount;
        }
        else
        {
            throw new Exception("Lines must be greater than 0. Audience must be equal or greater than 0 and Invoice cannot be null to this method");
        }

    }
    public double TragedyCalc(double amount, int lines, int audience, Invoice invoice)
    {
        if (lines > 0 && audience >= 0 && invoice is not null)
        {
            amount = AmountBaseTragedy(lines);

            if (audience > 30)
            {
                amount += AdditionalAmountTragedyOver30(audience);
                invoice.Customer.Credits += AddCredits(audience);
                invoice.Customer.PartialCredits += AddCredits(audience);
            }

            return amount;
        }
        else
        {
            throw new Exception("Lines must be greater than 0. Audience must be equal or greater than 0 and Invoice cannot be null to this method");
        }

    }
    public double HistoryCalc(double amount, int lines, int audience, Invoice invoice)
    {
        if (lines > 0 && audience >= 0 && invoice is not null)
        {
            amount = AmountBaseTragedy(lines);

            if (audience > 30)
                amount += AdditionalAmountTragedyOver30(audience);


            amount += AmountBaseComedy(lines, audience);

            if (audience > 20)
                amount += AdditionalAmountComedyOver20(audience);

            if (audience > 30)
            {
                invoice.Customer.Credits += AddCredits(audience);
                invoice.Customer.PartialCredits += AddCredits(audience);
            }

            return amount;
        }
        else
        {
            throw new Exception("Lines must be greater than 0. Audience must be equal or greater than 0 and Invoice cannot be null to this method");
        }

    }
}