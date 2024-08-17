using TheatricalPlayersRefactoringKata.Models;

namespace TheatricalPlayersRefactoringKata.Calculators
{
    internal interface IPlayCalculator
    {
        decimal CalculateAmount(Play play, Performance perf);
        int CalculateVolumeCredits(Play play, Performance perf);
    }
}
