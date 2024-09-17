using TP.Domain.Entities;

public abstract class PlayCalculatorBase : IPlayCalculator
{
    public abstract decimal CalculateAmount(Performance performance);

    public virtual int CalculateCredits(Performance performance)
    {
        return Math.Max(performance.Audience - 30, 0);
    }
}