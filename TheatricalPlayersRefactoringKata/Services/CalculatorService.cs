using TheatricalPlayersRefactoringKata.Entities;

namespace TheatricalPlayersRefactoringKata.Services;

public abstract class CalculatorService
{
    protected static int CalculateBaseAmount(int lines)
    { 
        if (lines < 1000) lines = 1000;

        if (lines > 4000) lines = 4000;

        return lines * 10;
    }
}
