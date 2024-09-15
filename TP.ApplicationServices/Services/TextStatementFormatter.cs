using TP.Domain.Entities;

public class TextStatementFormatter : IStatementFormatter
{
    private readonly StatementCalculator _calculator = new StatementCalculator();

    public string FormatStatement(Invoice invoice, Dictionary<string, Play> plays, decimal totalAmount, int volumeCredits)
    {
        var result = $"Statement for {invoice.CustomerName}\n";
        foreach (var perf in invoice.Performances)
        {
            var play = plays[perf.PlayId];
            var (amount, _) = _calculator.CalculateAmountAndCredits(play, perf);
            result += FormatPerformance(play, perf, amount);
        }
        result += FormatTotals(totalAmount, volumeCredits);
        return result;
    }

    public string FormatPerformance(Play play, Performance perf, decimal amount)
    {
        return $"  {play.Name}: {amount:F2} ({perf.Audience} seats)\n";
    }

    public string FormatTotals(decimal totalAmount, int volumeCredits)
    {
        return $"Amount owed is {totalAmount:F2}\nYou earned {volumeCredits} credits\n";
    }
}