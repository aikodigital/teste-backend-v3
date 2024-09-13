using TP.Domain.Entities;

public class ComedyCalculator : PlayCalculatorBase
{
    public override decimal CalculateAmount(Performance performance)
    {
        decimal baseAmount = 30000;
        if (performance.Audience > 20)
        {
            baseAmount += 10000 + 500 * (performance.Audience - 20);
        }
        baseAmount += 300 * performance.Audience;
        return baseAmount;
    }

    public override int CalculateCredits(Performance performance)
    {
        int credits = base.CalculateCredits(performance);
        credits += (int)Math.Floor((decimal)performance.Audience / 5);
        return credits;
    }
}