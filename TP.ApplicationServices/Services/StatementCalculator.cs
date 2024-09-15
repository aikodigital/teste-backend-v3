using TP.Domain.Entities;

public class StatementCalculator
{
    public (decimal Amount, int Credits) CalculateAmountAndCredits(Play play, Performance perf)
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
                var tragedyAmount = CalculateAmountAndCredits(new Play("Tragedy Play", play.Lines, "tragedy"), perf).Amount;
                var comedyAmount = CalculateAmountAndCredits(new Play("Comedy Play", play.Lines, "comedy"), perf).Amount;
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
}
