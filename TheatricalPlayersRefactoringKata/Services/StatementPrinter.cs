using System;
using System.Collections.Generic;
using System.Text;
using TheatricalPlayersRefactoringKata.Models;

namespace TheatricalPlayersRefactoringKata.Services;

public class StatementPrinter
{
    public string Print(Invoice invoice, Dictionary<string, Play> plays)
    {
        var result = new StringBuilder();
        decimal totalAmount = 0;
        int volumeCredits = 0;

        result.AppendLine($"Statement for {invoice.Customer}");

        foreach (var performance in invoice.Performances)
        {
            var play = plays[performance.PlayId];
            var calculator = PlayCalculatorFactory.CreateCalculator(performance, play);

            decimal thisAmount = calculator.CalculateAmount(performance);
            volumeCredits += calculator.CalculateVolumeCredits(performance);

            result.AppendLine($"  {play.Name}: {FormatCurrency(thisAmount)} ({performance.Audience} seats)");
            totalAmount += thisAmount;
        }

        result.AppendLine($"Amount owed is {FormatCurrency(totalAmount)}");
        result.AppendLine($"You earned {volumeCredits} credits");

        return result.ToString();
    }

    public string PrintXml(Invoice invoice, Dictionary<string, Play> plays)
    {
        var result = new StringBuilder();
        result.AppendLine($"<statement>");
        result.AppendLine($"  <customer>{invoice.Customer}</customer>");
        result.AppendLine($"  <performances>");

        decimal totalAmount = 0;
        int volumeCredits = 0;

        foreach (var performance in invoice.Performances)
        {
            var play = plays[performance.PlayId];
            var calculator = PlayCalculatorFactory.CreateCalculator(performance, play);

            decimal thisAmount = calculator.CalculateAmount(performance);
            volumeCredits += calculator.CalculateVolumeCredits(performance);

            result.AppendLine($"    <performance>");
            result.AppendLine($"      <play>{play.Name}</play>");
            result.AppendLine($"      <audience>{performance.Audience}</audience>");
            result.AppendLine($"      <amount>{FormatCurrency(thisAmount)}</amount>");
            result.AppendLine($"    </performance>");
            totalAmount += thisAmount;
        }

        result.AppendLine($"  </performances>");
        result.AppendLine($"  <totalAmount>{FormatCurrency(totalAmount)}</totalAmount>");
        result.AppendLine($"  <volumeCredits>{volumeCredits}</volumeCredits>");
        result.AppendLine($"</statement>");

        return result.ToString();
    }

    private string FormatCurrency(decimal amount)
    {
        return $"{(amount / 100):C}";
    }
}
