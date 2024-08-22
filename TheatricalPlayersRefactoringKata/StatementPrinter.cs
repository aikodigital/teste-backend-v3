using System;
using System.Collections.Generic;
using TheatricalPlayersRefactoringKata;

public class StatementPrinter
{
    private readonly Dictionary<string, ICalculatorStrategy> _calculatorStrategies;

    public StatementPrinter(Dictionary<string, ICalculatorStrategy> calculatorStrategies)
    {
        _calculatorStrategies = calculatorStrategies ?? throw new ArgumentNullException(nameof(calculatorStrategies));
    }

    public string Print(Invoice invoice, Dictionary<string, Play> plays)
    {
        if (invoice == null) throw new ArgumentNullException(nameof(invoice));
        if (plays == null) throw new ArgumentNullException(nameof(plays));

        var totalAmount = 0m;
        var totalCredits = 0;
        var result = $"Statement for {invoice.Customer}\n";

        foreach (var performance in invoice.Performances)
        {
            if (!plays.TryGetValue(performance.PlayId, out var play))
            {
                throw new ArgumentException($"Play with ID {performance.PlayId} not found.", nameof(plays));
            }

            if (!_calculatorStrategies.TryGetValue(play.Type, out var calculator))
            {
                throw new ArgumentException($"Calculator strategy for type {play.Type} not found.", nameof(_calculatorStrategies));
            }

            var amount = calculator.CalculateAmount(performance, play);
            var credits = calculator.CalculateCredits(performance, play);

            totalAmount += amount;
            totalCredits += credits;

            result += $"  {play.Name}: {amount:C} ({performance.Audience} seats)\n";
        }

        result += $"Amount owed is {totalAmount:C}\n";
        result += $"You earned {totalCredits} credits\n";
        return result;
    }
}
