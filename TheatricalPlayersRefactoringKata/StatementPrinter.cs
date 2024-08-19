using System;
using System.Collections.Generic;
using System.Globalization;

namespace TheatricalPlayersRefactoringKata;

public class StatementPrinter
{
    private const int MIN_LINES = 1000;
    private const int MAX_LINES = 4000;
    private const int BASE_COST_PERLINE = 10;
    private const int TRAGEDY_EXTRA_COST_PER_AUDIENCE = 1000;
    private const int COMEDY_BASE_COST = 10000;
    private const int COMEDY_EXTRA_COST_PER_AUDIENCE = 500;
    private const int COMEDY_ADDITIONAL_COST_PER_AUDIENCE = 300;
    private const int COMEDY_BONUS_THRES_HOLD = 20;
    private const int CREDIT_THRES_HOLD = 30;
    private const int COMEDY_BONUS_FACTOR = 5;

    public string Print(Invoice invoice, Dictionary<string, Play> plays)
    {
        var totalAmount = 0;
        var volumeCredits = 0;
        var result = string.Format("Statement for {0}\n", invoice.Customer);
        var cultureInfo = new CultureInfo("en-US");

        foreach (var perf in invoice.Performances)
        {
            var play = plays[perf.PlayId];
            var thisAmount = CalculateAmount(play, perf);
            volumeCredits += CalculateVolumeCredits(play, perf);

            result += string.Format(cultureInfo, "  {0}: {1:C} ({2} seats)\n", play.Name, thisAmount / 100m, perf.Audience);
            totalAmount += thisAmount;
        }

        result += string.Format(cultureInfo, "Amount owed is {0:C}\n", totalAmount / 100m);
        result += string.Format("You earned {0} credits\n", volumeCredits);
        return result;
    }

    private int CalculateAmount(Play play, Performance perf)
    {
        var lines = Math.Clamp(play.Lines, MIN_LINES, MAX_LINES);
        var thisAmount = lines * BASE_COST_PERLINE;

        switch (play.Type)
        {
            case "tragedy":
                if (perf.Audience > CREDIT_THRES_HOLD)
                {
                    thisAmount += TRAGEDY_EXTRA_COST_PER_AUDIENCE * (perf.Audience - CREDIT_THRES_HOLD);
                }
                break;

            case "comedy":
                if (perf.Audience > COMEDY_BONUS_THRES_HOLD)
                {
                    thisAmount += COMEDY_BASE_COST + COMEDY_EXTRA_COST_PER_AUDIENCE * (perf.Audience - COMEDY_BONUS_THRES_HOLD);
                }
                thisAmount += COMEDY_ADDITIONAL_COST_PER_AUDIENCE * perf.Audience;
                break;

            case "history":
                var tragedyAmount = CalculateAmount(new Play("tragedy", lines, "tragedy"), perf);
                var comedyAmount = CalculateAmount(new Play("comedy", lines, "comedy"), perf);
                thisAmount = tragedyAmount + comedyAmount;
                break;

            default:
                throw new Exception("unknown type: " + play.Type);
        }

        return thisAmount;
    }

    private int CalculateVolumeCredits(Play play, Performance perf)
    {
        var credits = Math.Max(perf.Audience - CREDIT_THRES_HOLD, 0);

        if (play.Type == "comedy")
        {
            credits += (int)Math.Floor((decimal)perf.Audience / COMEDY_BONUS_FACTOR);
        }

        return credits;
    }
}
