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
        CultureInfo cultureInfo = new CultureInfo("en-US");

        foreach(var performance in invoice.Performances)
        {
            var play = plays[performance.PlayId];
            int linesPlay = GetLinesPlay(play);

            var baseValue = linesPlay * 10;

            switch (play.Type)
            {
                case "tragedy":
                    if (performance.Audience > 30)
                    {
                        baseValue += 1000 * (performance.Audience - 30);
                    }
                    break;
                case "comedy":
                    if (performance.Audience > 20)
                    {
                        baseValue += 10000 + 500 * (performance.Audience - 20);
                    }
                    baseValue += 300 * performance.Audience;
                    break;
                default:
                    throw new Exception("unknown type: " + play.Type);
            }

            AddCredits(ref volumeCredits, performance, play);

            // print line for this order
            var performanceAmount = Convert.ToDecimal(baseValue / 100);

            invoice.PerformancesAmountCurtumer.Add(play.Name, performanceAmount);
            result += String.Format(cultureInfo, "  {0}: {1:C} ({2} seats)\n", play.Name, performanceAmount, performance.Audience);

            totalAmount += baseValue;
        }

        invoice.TotalAmount = Convert.ToDecimal(totalAmount / 100);
        invoice.VolumeCredits = volumeCredits;

        result += String.Format(cultureInfo, "Amount owed is {0:C}\n", invoice.TotalAmount);
        result += String.Format("You earned {0} credits\n", invoice.VolumeCredits);
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
