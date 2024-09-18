using System;
using System.Text;
using TheatricalPlayersRefactoringKata.Domain.Entities;
using TheatricalPlayersRefactoringKata.Domain.Interfaces;

namespace TheatricalPlayersRefactoringKata.Domain.Services.PlayTypeCalculators;

public class HistoryCalculator : IPlayTypeCalculator
{
    private readonly TragedyCalculator _tragedyCalculator = new TragedyCalculator();
    private readonly ComedyCalculator _comedyCalculator = new ComedyCalculator();

    public decimal CalculateAmount(Performance performance)
    {
        throw new NotImplementedException("baseAmount is required for HistoryCalculator.");
    }

    public decimal CalculateAmount(Performance performance, decimal baseAmount)
    {
        if (performance.Play == null)
            throw new Exception("Play data is required for calculation.");

        var teste = baseAmount; 

        var tragedyAmount = _tragedyCalculator.CalculateAmount(performance);
        var comedyAmount = _comedyCalculator.CalculateAmount(performance);

        decimal amount = tragedyAmount + comedyAmount + baseAmount;

        decimal adjustmentFactor = performance.Audience > 30 ? 1.39712m : 1.8432m;
        decimal adjustedAmount = Math.Ceiling(amount * adjustmentFactor);

        return adjustedAmount;
    }

    public  int CalculateVolumeCredits(Performance performance)
    {
        return Math.Max(performance.Audience - 30, 0);
    }
}
