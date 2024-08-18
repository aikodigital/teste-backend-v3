using System;
using System.Globalization;
using System.Threading;
using TheatricalPlayersRefactoringKata.Enums;
using TheatricalPlayersRefactoringKata.Models;

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

        if(audiance > 30)
            invoice.Customer.Credits += AddCredits(audiance);
        
        
        invoice.Customer.Credits += AddBonusCredit(audiance);

        if (audiance > 20)
            amount += AdditionalAmountComedyOver20(audiance);

        return amount;
    }
    public double TragedyCalc(double amount, int lines, int audiance, Invoice invoice)
    {
        amount = AmountBaseTragedy(lines);

        if (audiance > 30)
        {
            amount += AdditionalAmountTragedyOver30(audiance);
            invoice.Customer.Credits += AddCredits(audiance);
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
            invoice.Customer.Credits += AddCredits(audiance);

        return amount;
    }
}