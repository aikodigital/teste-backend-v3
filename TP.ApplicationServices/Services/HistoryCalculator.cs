using TP.Domain.Entities;

public class HistoryCalculator : PlayCalculatorBase
{
    private readonly TragedyCalculator _tragedyCalculator = new TragedyCalculator();
    private readonly ComedyCalculator _comedyCalculator = new ComedyCalculator();

    public override decimal CalculateAmount(Performance performance)
    {
        return _tragedyCalculator.CalculateAmount(performance) + _comedyCalculator.CalculateAmount(performance);
    }

    public override int CalculateCredits(Performance performance)
    {
        return _tragedyCalculator.CalculateCredits(performance) + _comedyCalculator.CalculateCredits(performance);
    }
}