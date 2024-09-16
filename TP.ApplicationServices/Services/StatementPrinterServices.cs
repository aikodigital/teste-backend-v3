using System.Globalization;
using TP.ApplicationServices.Interfaces;
using TP.Domain.Entities;

public class StatementPrinterServices : IStatementPrinterServices
{
    private readonly CultureInfo _usCulture = new CultureInfo("en-US");
    private readonly StatementCalculator _calculator = new StatementCalculator();

    public string Print(Invoice invoice, Dictionary<string, Play> plays, string format)
    {
        IStatementFormatter formatter = format switch
        {
            "xml" => new XmlStatementFormatter(),
            _ => new TextStatementFormatter(),
        };

        var totalAmount = 0m;
        var volumeCredits = 0;

        foreach (var perf in invoice.Performances)
        {
            var play = plays[perf.PlayId];
            var (thisAmount, credits) = _calculator.CalculateAmountAndCredits(play, perf);

            volumeCredits += credits;
            totalAmount += thisAmount;
        }

        return formatter.FormatStatement(invoice, plays, totalAmount, volumeCredits);
    }
}
