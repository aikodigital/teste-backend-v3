using System.Collections.Generic;
using TheatricalPlayersRefactoringKata;

public class StatementPrinter
{
    private readonly Dictionary<string, ICalculatorStrategy> _calculatorStrategies;

    public StatementPrinter()
    {
        _calculatorStrategies = new Dictionary<string, ICalculatorStrategy>
        {
            { "tragedy", new TragedyCalculator() },
            { "comedy", new ComedyCalculator() },
            { "historical", new HistoricalCalculator() }
        };
    }

    public string Print(Invoice invoice, Dictionary<string, Play> plays)
    {
        var totalAmount = 0m;
        var totalCredits = 0;
        var result = $"Statement for {invoice.Customer}\n";

        foreach (var performance in invoice.Performances)
        {
            var play = plays[performance.PlayId];
            var calculator = _calculatorStrategies[play.Type];
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
