using TheatricalPlayersRefactoringKata.Domain.Entities;

namespace TheatricalPlayersRefactoringKata.Domain.Interfaces
{
    public interface ITheatricalCalculator
    {
        decimal CalculateAmount(Performance perf, Play play);
        int CalculateVolumeCredits(Performance perf);
    }
}
