using System;
using TheatricalPlayersRefactoringKata.Models;

public class ComedyCalculator : PlayCalculator
{
    public ComedyCalculator(Performance performance, Play play)
        : base(performance, play) { }

    public override decimal CalculateAmount(Performance performance)
    {
        decimal amount = 30000;
        if (performance.Audience > 20)
        {
            amount += 10000 + 500 * (performance.Audience - 20);
        }
        amount += 300 * performance.Audience;
        return amount;
    }

    public override int CalculateVolumeCredits(Performance performance)
    {
        return Math.Max(performance.Audience - 30, 0) + (performance.Audience / 5);
    }
}
