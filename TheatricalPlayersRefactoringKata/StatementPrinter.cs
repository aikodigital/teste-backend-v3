using System;
using System.Collections.Generic;
using System.Globalization;

namespace TheatricalPlayersRefactoringKata;

public class StatementPrinter
{
    public string Print(Invoice invoice, Dictionary<string, Play> plays)
    {
        var totalAmount = 0;
        var volumeCredits = 0;
        var result = $"Statement for {invoice.Customer}\n";
        CultureInfo cultureInfo = new CultureInfo("en-US");

        foreach (var perf in invoice.Performances)
        {
            var play = plays[perf.PlayId];
            var thisAmount = CalculateAmount(perf, play);
            totalAmount += thisAmount;

            volumeCredits += CalculateVolumeCredits(perf, play);

            result += string.Format(cultureInfo, "  {0}: {1:C} ({2} seats)\n", play.Name, thisAmount / 100m, perf.Audience);
        }

        result += string.Format(cultureInfo, "Amount owed is {0:C}\n", totalAmount / 100m);
        result += $"You earned {volumeCredits} credits\n";

        return result;
    }

    private int CalculateAmount(Performance perf, Play play)
    {
        var lines = Math.Clamp(play.Lines, 1000, 4000);
        var baseAmount = lines * 10;

        return play.Type switch
        {
            "tragedy" => baseAmount + (perf.Audience > 30 ? 1000 * (perf.Audience - 30) : 0),
            "comedy" => baseAmount + 300 * perf.Audience + (perf.Audience > 20 ? 10000 + 500 * (perf.Audience - 20) : 0),
            "history" => CalculateHistoryAmount(perf, lines),
            _ => throw new Exception("Unknown type: " + play.Type)
        };
    }

    private int CalculateHistoryAmount(Performance perf, int lines)
    {
        var tragedyAmount = lines * 10 + (perf.Audience > 30 ? 1000 * (perf.Audience - 30) : 0);
        var comedyAmount = lines * 10 + 300 * perf.Audience + (perf.Audience > 20 ? 10000 + 500 * (perf.Audience - 20) : 0);
        return tragedyAmount + comedyAmount;
    }

    private int CalculateVolumeCredits(Performance perf, Play play)
    {
        var credits = Math.Max(perf.Audience - 30, 0);
        if (play.Type == "comedy")
        {
            credits += (int)Math.Floor((decimal)perf.Audience / 5);
        }
        return credits;
    }
}
