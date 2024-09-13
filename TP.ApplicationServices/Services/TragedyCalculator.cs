using TP.Domain.Entities;

public class TragedyCalculator : PlayCalculatorBase
{
    public override decimal CalculateAmount(Performance performance)
    {
        decimal baseAmount = 40000;
        if (performance.Audience > 30)
        {
            baseAmount += 1000 * (performance.Audience - 30);
        }
        return baseAmount;
    }
}