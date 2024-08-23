using System;
using System.Collections.Generic;
using System.Globalization;

namespace TheatricalPlayersRefactoringKata;

public struct ValueCredit
{
    public string Name;
    public decimal Value;
    public int Credit;

}

public class CalculatedAmount
{
    const int MaxLines = 4000;
    const int MinLines = 1000;
    public List<ValueCredit> Calculated(Invoice invoice, Dictionary<string, Play> plays)
    {
        var volumeCredits = 0;
        var result = new List<ValueCredit>();
        CultureInfo cultureInfo = new CultureInfo("en-US");

        foreach (var currentPerformance in invoice.Performances)
        {
            var play = plays[currentPerformance.PlayId];
            var lines = play.Lines;

            if (lines < MinLines) lines = MinLines;
            if (lines > MaxLines) lines = MaxLines;

            decimal baseValue = (decimal)lines / 10;
            switch (play.Type)
            {
                case "tragedy":
                    baseValue = CalculationTragedy(currentPerformance.Audience, baseValue);
                    break;
                case "comedy":
                    baseValue = CalculationComedy(currentPerformance.Audience, baseValue);
                    break;
                case "history":
                    baseValue = CalculationHistory(currentPerformance.Audience, baseValue);
                    break;
                default:
                    throw new Exception("unknown type: " + play.Type);
            }
            // add volume credits
            volumeCredits = Math.Max(currentPerformance.Audience - 30, 0);
            // add extra credit for every ten comedy attendees
            if ("comedy" == play.Type) volumeCredits += (int)Math.Floor((decimal)currentPerformance.Audience / 5);

            // print line for this order

            result.Add(new ValueCredit() { Credit = volumeCredits, Value = baseValue, Name = currentPerformance.PlayId });
        }
        return result;
    }

    private decimal CalculationHistory(int audience, decimal value)
    {
        return CalculationComedy(audience, value) + CalculationTragedy(audience, value);
    }

    private decimal CalculationComedy(int audience, decimal value)
    {
        if (audience > 20)
        {
            value += 100 + 5 * (audience - 20);
        }
        value += 3 * audience;
        return value;
    }

    private decimal CalculationTragedy(int audience, decimal value)
    {
        if (audience > 30)
        {
            value += 10 * (audience - 30);
        }

        return value;
    }
}
