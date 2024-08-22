using TheatricalPlayersRefactoringKata.Core.Entities;

namespace TheatricalPlayersRefactoringKata.Core.Interfaces;

public interface ICalculator
{
    public static abstract int CalculateAmount(Performance perf, Play? play);
    protected static int DefaultAmount(int lines)
    {
        return lines switch
        {
            < 1000 => 1000 * 10,
            > 4000 => 4000 * 10,
            _ => lines * 10
        };
    }
}