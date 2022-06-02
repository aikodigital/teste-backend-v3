using System;
using System.Collections.Generic;
using System.Globalization;

namespace TheatricalPlayersRefactoringKata;

//Impressora de extratos
public class StatementPrinter
{
    private const int TRAGEDY_ADICIONAL_AUDIENCE_VALUE = 10;
    private const int TRAGEDY_MAX_AUDIENCE = 30;

    private const int COMEDY_DEFAULT_AUDIENCE_VALUE = 3;
    private const int COMEDY_ADICIONAL_AUDIENCE_VALUE = 5;
    private const int COMEDY_ADICIONAL_AUDIENCE_VALUE_INCREASED = 100; 
    private const int COMEDY_MAX_AUDIENCE = 20;
    private const int COMEDY_AUDIENCE_DIVISION_CREDIT = 5;

    private const int AUDIENCE_FROM_AT_LEAST_FOR_CREDIT = 30;
    
    private const int LINE_MIN = 1000;
    private const int LINE_MAX = 4000;

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
            result = PrintLineThisOrder(invoice, result, cultureInfo, performance, play, ref baseValue);

            totalAmount += baseValue;
        }

        invoice.TotalAmount = totalAmount;
        invoice.VolumeCredits = volumeCredits;

        result += String.Format(cultureInfo, "Amount owed is {0:C}\n", invoice.TotalAmount);
        result += String.Format("You earned {0} credits\n", invoice.VolumeCredits);
        return result;
    }

    private int CalculateComedyValue(Performance performance, int baseValue)
    {
        if (performance.Audience > COMEDY_MAX_AUDIENCE)
        {
            baseValue += COMEDY_ADICIONAL_AUDIENCE_VALUE_INCREASED +
                         COMEDY_ADICIONAL_AUDIENCE_VALUE * (performance.Audience - COMEDY_MAX_AUDIENCE);
        }

        baseValue += COMEDY_DEFAULT_AUDIENCE_VALUE * performance.Audience;
        
        return baseValue;
    }

    private int CalculateTragedyValue(Performance performance, int baseValue)
    {
        if (performance.Audience < TRAGEDY_MAX_AUDIENCE) return baseValue;

        baseValue += TRAGEDY_ADICIONAL_AUDIENCE_VALUE * (performance.Audience - TRAGEDY_MAX_AUDIENCE);

        return baseValue;
    }

    private int CalculateBaseValue(Play play)
    {
        int linesPlay = GetLinesPlay(play);
        var baseValue = linesPlay / 10;

        return baseValue;
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
        if (play.Type != "comedy") return;

        volumeCredits += (int)Math.Floor((decimal)performance.Audience / COMEDY_AUDIENCE_DIVISION_CREDIT);
    }

    private void AddCreditsForAudience(ref int volumeCredits, Performance performance)
    {
        volumeCredits += Math.Max(performance.Audience - AUDIENCE_FROM_AT_LEAST_FOR_CREDIT, 0);
    }

    private int GetLinesPlay(Play play)
    {
        var lines = play.Lines;

        if (play.Lines < LINE_MIN) lines = LINE_MIN;
        if (play.Lines > LINE_MAX) lines = LINE_MAX;

        return lines;
    }
}
