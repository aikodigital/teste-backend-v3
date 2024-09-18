using System;
using TheatricalPlayersRefactoringKata.Domain.Entities;
using TheatricalPlayersRefactoringKata.Domain.Interfaces;

namespace TheatricalPlayersRefactoringKata.Domain.Services.PlayTypeCalculators;

public class ComedyCalculator : IPlayTypeCalculator
{
    public decimal CalculateAmount(Performance performance)
    {
        if (performance.Play == null)
            throw new Exception("Play data is required for calculation.");

        decimal amount = 0;
        if (performance.Audience > 20)
        {
            amount += 10000 + 500 * (performance.Audience - 20);
        }
        amount += 300 * performance.Audience;
        return amount;
    }

    public int CalculateVolumeCredits(Performance performance)
    {
        return Math.Max(performance.Audience - 30, 0) + (int)Math.Floor((decimal)performance.Audience / 5);
    }
}
