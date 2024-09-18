using System;
using System.Collections.Generic;
using System.Globalization;
using TheatricalPlayersRefactoringKata.Factory;
using TheatricalPlayersRefactoringKata.Formatters.Interface;
using TheatricalPlayersRefactoringKata.Models;

namespace TheatricalPlayersRefactoringKata;

public class StatementPrinter
{
    private readonly IExtratoFormatter _extratatoFormatter;
    public StatementPrinter(IExtratoFormatter extratoFormatter)
    {
        _extratatoFormatter = extratoFormatter;
    }

    public string Print(Invoice invoice, Dictionary<string, Play> plays)
    {
        decimal totalAmount = 0;
        var volumeCredits = 0;
        string resultPerformace = string.Empty;
        var resultCustomer = _extratatoFormatter.FormatCustomer(invoice.Customer);
        CultureInfo cultureInfo = new CultureInfo("en-US");

        foreach (var perf in invoice.Performances)
        {
            var play = plays[perf.PlayId];
            var lines = play.Lines;

            var calculator = PlayCalculatorFactory.GetCalculator(play);
            var thisAmount = calculator.CalculateAmount(perf, play);
            var performaceCredits = calculator.CalculateCredits(perf);
            volumeCredits += performaceCredits;
            resultPerformace += _extratatoFormatter.FormatPerformance(play, perf, thisAmount, performaceCredits, cultureInfo);
            totalAmount += thisAmount;
        }
        var resultTotalAmount = _extratatoFormatter.FormatTotalAmount(totalAmount, cultureInfo);
        var resultTotalCredits = _extratatoFormatter.FormatTotalCredits(volumeCredits);
        var result = _extratatoFormatter.GenerateStatement(resultCustomer, resultPerformace, resultTotalAmount, resultTotalCredits, cultureInfo);
        return result;
    }
}
