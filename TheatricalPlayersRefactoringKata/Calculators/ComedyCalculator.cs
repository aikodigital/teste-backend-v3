using System;
using TheatricalPlayersRefactoringKata;

public class ComedyCalculator : PlayCalculator
{
    public ComedyCalculator(Performance performance, Play play)
        : base(performance, play)
    {
    }

    public override decimal CalculateAmount()
    {
        decimal baseAmount = CalculateBaseAmount();
        baseAmount += 3m * Performance.Audience;

        if (Performance.Audience > 20)
        {
            baseAmount += 100m + 5m * (Performance.Audience - 20);
        }

        return baseAmount;
    }

    public override int CalculateVolumeCredits()
    {
        return Math.Max(Performance.Audience - 30, 0) + (int)Math.Floor((decimal)Performance.Audience / 5);
    }
}

