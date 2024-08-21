using System;
using TheatricalPlayersRefactoringKata;

public abstract class PlayCalculator
{
    protected Performance Performance;
    protected Play Play;

    protected PlayCalculator(Performance performance, Play play)
    {
        Performance = performance;
        Play = play;
    }

    protected decimal CalculateBaseAmount()
{
    
    int lines = Play.Lines;
    if (lines < 1000) lines = 1000;
    if (lines > 4000) lines = 4000;
    
    
    return lines / 10m; 
}

    public abstract decimal CalculateAmount();
    public abstract int CalculateVolumeCredits();
}


