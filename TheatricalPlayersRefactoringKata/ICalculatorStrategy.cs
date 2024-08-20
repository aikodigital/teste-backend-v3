using System;
using TheatricalPlayersRefactoringKata;

public interface ICalculatorStrategy
{
    decimal CalculateAmount(Performance performance, Play play);
    int CalculateCredits(Performance performance, Play play);
}

public class TragedyCalculator : ICalculatorStrategy
{
    public decimal CalculateAmount(Performance performance, Play play)
    {
        var baseAmount = Math.Clamp(play.NumberOfLines / 10, 1000, 4000);
        if (performance.Audience > 30)
        {
            baseAmount += 10 * (performance.Audience - 30);
        }
        return baseAmount;
    }

    public int CalculateCredits(Performance performance, Play play)
    {
        return performance.Audience > 30 ? performance.Audience - 30 : 0;
    }
}

public class ComedyCalculator : ICalculatorStrategy
{
    public decimal CalculateAmount(Performance performance, Play play)
    {
        var baseAmount = Math.Clamp(play.NumberOfLines / 10, 1000, 4000);
        baseAmount += 3 * performance.Audience;
        if (performance.Audience > 20)
        {
            baseAmount += 100 + 5 * (performance.Audience - 20);
        }
        return baseAmount;
    }

    public int CalculateCredits(Performance performance, Play play)
    {
        var credits = performance.Audience > 30 ? performance.Audience - 30 : 0;
        credits += performance.Audience / 5;
        return credits;
    }
}

public class HistoricalCalculator : ICalculatorStrategy
{
    private readonly TragedyCalculator _tragedyCalculator = new TragedyCalculator();
    private readonly ComedyCalculator _comedyCalculator = new ComedyCalculator();

    public decimal CalculateAmount(Performance performance, Play play)
    {
        return _tragedyCalculator.CalculateAmount(performance, play) +
               _comedyCalculator.CalculateAmount(performance, play);
    }

    public int CalculateCredits(Performance performance, Play play)
    {
        return _tragedyCalculator.CalculateCredits(performance, play) +
               _comedyCalculator.CalculateCredits(performance, play);
    }
}
