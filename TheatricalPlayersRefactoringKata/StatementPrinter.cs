using System;
using System.Collections.Generic;
using System.Globalization;

namespace TheatricalPlayersRefactoringKata;

//Impressora de extratos
public class StatementPrinter
{
    private const int COMEDY_AUDIENCE_DIVISION_CREDIT = 5;

    private const int AUDIENCE_FROM_AT_LEAST_FOR_CREDIT = 30;


    public string Print(Invoice invoice, Dictionary<string, Play> plays)
    {
        var totalAmount = 0;
        var volumeCredits = 0;

        var result = string.Format("Statement for {0}\n", invoice.Customer);
        var cultureInfo = new CultureInfo("en-US");

        foreach(var performance in invoice.Performances)
        {
            var play = plays[performance.PlayId];

            var baseValue = play.CalculateBaseValue(performance);

            AddCredits(ref volumeCredits, performance, play);
            result = PrintLineThisOrder(invoice, result, cultureInfo, performance, play, ref baseValue);

            totalAmount += baseValue;
        }

        invoice.TotalAmount = totalAmount;
        invoice.VolumeCredits = volumeCredits;

        result += String.Format(cultureInfo, "Amount owed is {0:C}\n", invoice.TotalAmount);
        result += String.Format("You earned {0} credits\n", invoice.VolumeCredits);
        return result;
    }

    private string PrintLineThisOrder(Invoice invoice, string result, CultureInfo cultureInfo,
                                      Performance performance, Play play, ref int performanceAmount)
    {
        invoice.PerformancesAmountCurtumer.Add(play.Name, performanceAmount);
        result += String.Format(cultureInfo, "  {0}: {1:C} ({2} seats)\n", play.Name, performanceAmount, performance.Audience);
        return result;
    }

    private void AddCredits(ref int volumeCredits, Performance performance, Play play)
    {
        AddCreditsForAudience(ref volumeCredits, performance);
        AddCreditForCommedyAudience(ref volumeCredits, performance, play);
    }

    private void AddCreditForCommedyAudience(ref int volumeCredits, Performance performance, Play play)
    {
        if (play is not ComedyPlay) return;

        volumeCredits += (int)Math.Floor((decimal)performance.Audience / COMEDY_AUDIENCE_DIVISION_CREDIT);
    }

    private void AddCreditsForAudience(ref int volumeCredits, Performance performance)
    {
        volumeCredits += Math.Max(performance.Audience - AUDIENCE_FROM_AT_LEAST_FOR_CREDIT, 0);
    }

}
