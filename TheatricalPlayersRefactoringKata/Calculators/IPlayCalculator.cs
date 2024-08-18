using TheatricalPlayersRefactoringKata.Models;

namespace TheatricalPlayersRefactoringKata.Calculators
{
    public interface IPlayCalculator
    {
        decimal CalculateAmount(Play play, Performance perf);
        int CalculateVolumeCredits(Play play, Performance perf);
    }
}
