using System;
using System.Collections.Generic;
using System.Globalization;

namespace TheatricalPlayersRefactoringKata;

//Impressora de extratos
public class StatementPrinter
{
    public string Print(Invoice invoice, Dictionary<string, Play> plays)
    {
        var totalAmount = 0;
        var volumeCredits = 0;

        var result = string.Format("Statement for {0}\n", invoice.Customer);
        var cultureInfo = new CultureInfo("en-US");

        foreach(var performance in invoice.Performances)
        {
            var play = plays[performance.PlayId];
            int baseValue = CalculateBaseValue(play);

            switch (play.Type)
            {
                case "tragedy":
                    baseValue = CalculateTragedyValue(performance, baseValue);
                    break;
                case "comedy":
                    baseValue = CalculateComedyValue(performance, baseValue);
                    break;
                default:
                    throw new Exception("unknown type: " + play.Type);
            }

            AddCredits(ref volumeCredits, performance, play);
            result = PrintLineThisOrder(invoice, result, cultureInfo, performance, play, baseValue);

            totalAmount += baseValue;
        }

        invoice.TotalAmount = Convert.ToDecimal(totalAmount / 100);
        invoice.VolumeCredits = volumeCredits;

        result += String.Format(cultureInfo, "Amount owed is {0:C}\n", invoice.TotalAmount);
        result += String.Format("You earned {0} credits\n", invoice.VolumeCredits);
        return result;
    }

    private int CalculateComedyValue(Performance performance, int baseValue)
    {
        if (performance.Audience > 20)
        {
            baseValue += 10000 + 500 * (performance.Audience - 20);
        }

        baseValue += 300 * performance.Audience;
        
        return baseValue;
    }

    private int CalculateTragedyValue(Performance performance, int baseValue)
    {
        if (performance.Audience < 30) return baseValue;

        baseValue += 1000 * (performance.Audience - 30);

        return baseValue;
    }

    private int CalculateBaseValue(Play play)
    {
        int linesPlay = GetLinesPlay(play);
        var baseValue = linesPlay * 10;

        return baseValue;
    }

    private string PrintLineThisOrder(Invoice invoice, string result, CultureInfo cultureInfo,
                                      Performance performance, Play play, int baseValue)
    {
        var performanceAmount = Convert.ToDecimal(baseValue / 100);

        invoice.PerformancesAmountCurtumer.Add(play.Name, performanceAmount);
        result += String.Format(cultureInfo, "  {0}: {1:C} ({2} seats)\n", play.Name, performanceAmount, performance.Audience);
        return result;
    }

    private void AddCredits(ref int volumeCredits, Performance performance, Play play)
    {
        AddVolumeCredits(ref volumeCredits, performance);
        AddExtraCreditForEveryTenCommedyAttendees(ref volumeCredits, performance, play);
    }

    private void AddExtraCreditForEveryTenCommedyAttendees(ref int volumeCredits, Performance performance, Play play)
    {
        if ("comedy" == play.Type)
            volumeCredits += (int)Math.Floor((decimal)performance.Audience / 5);
    }

    private void AddVolumeCredits(ref int volumeCredits, Performance performance)
    {
        volumeCredits += Math.Max(performance.Audience - 30, 0);
    }

    private int GetLinesPlay(Play play)
    {
        var lines = play.Lines;

        if (play.Lines < 1000) lines = 1000;
        if (play.Lines > 4000) lines = 4000;

        return lines;
    }
}
