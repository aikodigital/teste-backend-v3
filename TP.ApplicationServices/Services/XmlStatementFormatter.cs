using TP.Domain.Entities;

public class XmlStatementFormatter : IStatementFormatter
{
    private readonly StatementCalculator _calculator = new StatementCalculator();

    public string FormatStatement(Invoice invoice, Dictionary<string, Play> plays, decimal totalAmount, int volumeCredits)
    {
        var result = "<?xml version=\"1.0\" encoding=\"utf-8\"?>\n<Statement xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">\n";
        foreach (var perf in invoice.Performances)
        {
            var play = plays[perf.PlayId];
            var (amount, credits) = _calculator.CalculateAmountAndCredits(play, perf);
            result += FormatPerformance(play, perf, amount, credits);
        }
        result += FormatTotals(totalAmount, volumeCredits);
        result += "</Statement>";
        return result;
    }

    public string FormatPerformance(Play play, Performance perf, decimal amount, int credits)
    {
        return $"  <Item>\n    <AmountOwed>{amount:F2}</AmountOwed>\n    <EarnedCredits>{credits}</EarnedCredits>\n    <Seats>{perf.Audience}</Seats>\n  </Item>\n";
    }

    public string FormatTotals(decimal totalAmount, int volumeCredits)
    {
        return $"  <AmountOwed>{totalAmount:F2}</AmountOwed>\n  <EarnedCredits>{volumeCredits}</EarnedCredits>\n";
    }
}