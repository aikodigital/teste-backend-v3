using System;
using System.Collections.Generic;
using System.Globalization;
using TheatricalPlayersRefactoringKata.Calculators.Interfaces;
using TheatricalPlayersRefactoringKata.Calculators;
using TheatricalPlayersRefactoringKata.Models;
using System.Text;
using System.Linq;

namespace TheatricalPlayersRefactoringKata.Services;

public class StatementPrinter
{
    public static IPlayCalculator GetCalculatorByType(string playType) => playType.ToLower() switch
    {
        "tragedy" => new TragedyCalculator(),
        "comedy" => new ComedyCalculator(),
        "history" => new HistoryCalculator(),
        _ => throw new Exception("unknown type: " + playType),
    };

    public string PrintText(Invoice invoice, Dictionary<string, Play> plays)
    {
        var billingStatement = new StringBuilder($"Statement for {invoice.Customer}\n");
        try
        {
            decimal totalAmount = 0m;
            decimal volumeCredits = 0;
            CultureInfo cultureInfo = new CultureInfo("en-US");
            IPlayCalculator billTypeCalculator;

            foreach (Performance perf in invoice.Performances)
            {
                var play = plays.FirstOrDefault(p => p.Key.ToLower() == perf.PlayId).Value;

                if (play is null) throw new ArgumentOutOfRangeException($"{perf.PlayId} is not a valid Play.");

                billTypeCalculator = GetCalculatorByType(play.Type);
                decimal thisAmount = billTypeCalculator.CalculateAmount(play, perf.Audience);
                volumeCredits += billTypeCalculator.CalculateCredits(play, perf.Audience);

                billingStatement.AppendLine(cultureInfo, $"  {play.Name}: {thisAmount:C} ({perf.Audience} seats)");
                totalAmount += thisAmount;
            }
            billingStatement.AppendLine(cultureInfo, $"Amount owed is {totalAmount:C}");
            billingStatement.AppendLine($"You earned {volumeCredits} credits");
        }
        catch { throw; }
        return billingStatement.ToString();
    }
}
