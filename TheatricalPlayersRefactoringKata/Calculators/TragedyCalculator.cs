using System;
using TheatricalPlayersRefactoringKata;

public class TragedyCalculator : PlayCalculator
{
    public TragedyCalculator(Performance performance, Play play) : base(performance, play) { }

    public override decimal CalculateAmount()
    {
        decimal baseAmount = CalculateBaseAmount();
        if (Performance.Audience > 30)
        {
            baseAmount += 10 * (Performance.Audience - 30);
        }
        return baseAmount;
    }

    public override int CalculateVolumeCredits()
    {
        return Math.Max(Performance.Audience - 30, 0);
    }

}

