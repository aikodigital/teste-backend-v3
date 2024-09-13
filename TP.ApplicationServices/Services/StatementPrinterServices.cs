using System.Globalization;
using TP.ApplicationServices.Interfaces;
using TP.Domain.Entities;

public class StatementPrinterServices : IStatementPrinterServices
{
    private readonly CultureInfo _usCulture = new CultureInfo("en-US");

    public string Print(Invoice invoice, Dictionary<string, Play> plays, string format = "text")
    {
        var totalAmount = 0m;
        var volumeCredits = 0;
        var result = format == "xml"
            ? "<?xml version=\"1.0\" encoding=\"utf-8\"?>\n<Statement xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">\n"
            : $"Statement for {invoice.CustomerName}\n";

        foreach (var perf in invoice.Performances)
        {
            var play = plays[perf.PlayId];
            var (thisAmount, credits) = CalculateAmountAndCredits(play, perf);

            volumeCredits += credits;
            totalAmount += thisAmount;

            result += FormatPerformance(play, perf, thisAmount, format);
        }

        result += FormatTotals(totalAmount, volumeCredits, format);
        result += format == "xml" ? "</Statement>" : "";

        return result;
    }

    private (decimal, int) CalculateAmountAndCredits(Play play, Performance perf)
    {
        decimal thisAmount;
        var volumeCredits = 0;

        var baseAmount = AdjustLines(play.Lines) / 10m;

        switch (play.Type)
        {
            case "tragedy":
                thisAmount = baseAmount;
                if (perf.Audience > 30)
                {
                    thisAmount += 10m * (perf.Audience - 30);
                }
                break;

            case "comedy":
                thisAmount = baseAmount + 3m * perf.Audience;
                if (perf.Audience > 20)
                {
                    thisAmount += 100m + 5m * (perf.Audience - 20);
                }
                volumeCredits += (int)Math.Floor(perf.Audience / 5m);
                break;

            case "history":
                var tragedyAmount = CalculateAmountAndCredits(new Play("Tragedy Play", play.Lines, "tragedy"), perf).Item1;
                var comedyAmount = CalculateAmountAndCredits(new Play("Comedy Play", play.Lines, "comedy"), perf).Item1;
                thisAmount = tragedyAmount + comedyAmount;
                break;

            default:
                throw new Exception($"unknown type: {play.Type}");
        }

        if (perf.Audience > 30)
        {
            volumeCredits += perf.Audience - 30;
        }

        return (thisAmount, volumeCredits);
    }

    private decimal AdjustLines(int lines)
    {
        if (lines < 1000) return 1000;
        if (lines > 4000) return 4000;
        return lines;
    }

    private string FormatPerformance(Play play, Performance perf, decimal amount, string format)
    {
        var formattedAmount = format == "xml"
            ? amount.ToString("F2", CultureInfo.InvariantCulture)
            : FormatCurrency(amount, format);

        return format == "xml"
            ? $"  <Item>\n    <AmountOwed>{formattedAmount}</AmountOwed>\n    <EarnedCredits>{CalculateAmountAndCredits(play, perf).Item2}</EarnedCredits>\n    <Seats>{perf.Audience}</Seats>\n  </Item>\n"
            : $"  {play.Name}: {formattedAmount} ({perf.Audience} seats)\n";
    }

    private string FormatCurrency(decimal amount, string format)
    {
        return format == "xml" || format == "usd"
            ? $"{amount:F2}"
            : $"R$ {amount:F2}".Replace('.', ','); // Formatação para R$
    }

    private string FormatTotals(decimal totalAmount, int volumeCredits, string format)
    {
        var formattedAmount = FormatCurrency(totalAmount, format);
        return format == "xml"
            ? $"  <AmountOwed>{formattedAmount}</AmountOwed>\n  <EarnedCredits>{volumeCredits}</EarnedCredits>\n"
            : $"Amount owed is {formattedAmount}\nYou earned {volumeCredits} credits\n";
    }
}
