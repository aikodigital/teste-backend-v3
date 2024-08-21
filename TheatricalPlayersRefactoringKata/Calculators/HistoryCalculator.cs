using System;
using TheatricalPlayersRefactoringKata;


public class HistoryCalculator : PlayCalculator
{
    private readonly Play _tragedyPlay;
    private readonly Play _comedyPlay;

    public HistoryCalculator(Performance performance, Play tragedyPlay, Play comedyPlay)
        : base(performance, null)
    {
        _tragedyPlay = tragedyPlay;
        _comedyPlay = comedyPlay;
    }

    public override decimal CalculateAmount()
    {
        var tragedyCalculator = new TragedyCalculator(Performance, _tragedyPlay);
        var comedyCalculator = new ComedyCalculator(Performance, _comedyPlay);

        var tragedyAmount = tragedyCalculator.CalculateAmount();
        var comedyAmount = comedyCalculator.CalculateAmount();

        return tragedyAmount + comedyAmount;
    }

   public override int CalculateVolumeCredits()
    {
        return Math.Max(Performance.Audience - 30, 0);
    }
}

