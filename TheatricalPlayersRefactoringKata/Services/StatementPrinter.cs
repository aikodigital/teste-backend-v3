using System;
using System.Collections.Generic;
using System.Globalization;
using TheatricalPlayersRefactoringKata.Calculators.Interfaces;
using TheatricalPlayersRefactoringKata.Models;

namespace TheatricalPlayersRefactoringKata.Services;

public class StatementPrinter
{
    private readonly ITypeCalculatorFactory _calculatorFactory;

    public StatementPrinter(ITypeCalculatorFactory calculatorFactory)
    {
        _calculatorFactory = calculatorFactory;
    }

    public string Print(Invoice invoice, Dictionary<string, Play> plays)
    {
        double totalAmount = 0;
        double volumeCredits = 0;
        var result = string.Format("Statement for {0}\n", invoice.Customer);
        CultureInfo cultureInfo = new CultureInfo("en-US");

        foreach (var perf in invoice.Performances)
        {
            var play = plays[perf.PlayId];
            perf.Play = play;

            var calculator = _calculatorFactory.GetCalculator(play.Type);
            var thisAmount = calculator.Calculate(perf);
            var thisVolumeCredits = calculator.CalculateCredits(perf);

            //add volume credits
            volumeCredits += thisVolumeCredits;

            // print line for this order
            result += string.Format(cultureInfo, "  {0}: {1:C} ({2} seats)\n", play.Name, Convert.ToDecimal(thisAmount / 100), perf.Audience);
            totalAmount += thisAmount;
        }
        result += string.Format(cultureInfo, "Amount owed is {0:C}\n", Convert.ToDecimal(totalAmount / 100));
        result += string.Format("You earned {0} credits\n", volumeCredits);
        return result;
    }
}
