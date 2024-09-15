using System;
using TheatricalPlayersRefactoringKata.Models;

public abstract class PlayCalculator : IPlayCalculator
{
    protected Performance Performance;
    protected Play Play;

    protected readonly Performance performance;
    protected readonly Play play;

    protected PlayCalculator(Performance performance, Play play)
    {
        this.performance = performance;
        this.play = play;
    }


    public virtual int CalculateVolumeCredits(Performance performance)
    {
        return Math.Max(performance.Audience - 30, 0);
    }

    public abstract decimal CalculateAmount(Performance performance);

}
