using System;
using System.Collections.Generic;
using System.Globalization;
using TheatricalPlayersRefactoringKata;

public class StatementPrinter
{
    public string Print(Invoice invoice, Dictionary<string, Play> plays)
{
    decimal totalAmount = 0;
    int volumeCredits = 0;
    var result = $"Statement for {invoice.Customer}\n";
    CultureInfo cultureInfo = new CultureInfo("en-US");

    foreach (var perf in invoice.Performances)
    {
        var play = plays[perf.PlayId];
        var calculator = CreatePlayCalculator(perf, play);
        var thisAmount = calculator.CalculateAmount();
        var thisCredits = calculator.CalculateVolumeCredits();

       
        volumeCredits += thisCredits;

       
        result += string.Format(cultureInfo, "  {0}: {1:C} ({2} seats)\n", play.Name, thisAmount, perf.Audience);
        totalAmount += thisAmount;
    }
    result += string.Format(cultureInfo, "Amount owed is {0:C}\n", totalAmount);
    result += string.Format("You earned {0} credits\n", volumeCredits);
    return result;
}

private PlayCalculator CreatePlayCalculator(Performance perf, Play play)
{
    switch (play.Type)
    {
        case "tragedy":
            return new TragedyCalculator(perf, play);
        case "comedy":
            return new ComedyCalculator(perf, play);
        case "history":
            return new HistoryCalculator(perf, play, play);
        default:
            throw new Exception("unknown type: " + play.Type);
    }
}
}
