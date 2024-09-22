using System;
using TheatricalPlayersRefactoringKata.Entities;

namespace TheatricalPlayersRefactoringKata.Services;

public abstract class CalculatorService
{
    public abstract int CalculateAmount(Performance performance, Play play);

    public virtual int CalculateCredits(Performance performance, Play play) =>
       Math.Max(performance.Audience - 30, 0);

    protected static int CalculateBaseAmount(int lines)
    { 
        if (lines < 1000) lines = 1000;

        if (lines > 4000) lines = 4000;

        return lines * 10;
    }
}
