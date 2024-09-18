using System;
using TheatricalPlayersRefactoringKata.Domain.Entities;
using TheatricalPlayersRefactoringKata.Domain.Interfaces;

namespace TheatricalPlayersRefactoringKata.Domain.Services.PlayTypeCalculators;

public class TragedyCalculator : IPlayTypeCalculator
{
    public decimal CalculateAmount(Performance performance)
    {
        if (performance.Play == null)
            throw new Exception("Play data is required for calculation.");

        decimal amount = 0;
        if (performance.Audience > 30)
        {
            amount += 1000 * (performance.Audience - 30);
        }
        return amount;
    }

    public int CalculateVolumeCredits(Performance performance)
    {
        return Math.Max(performance.Audience - 30, 0);
    }
}
