using TheatricalPlayersRefactoringKata.Domain.Entities;

namespace TheatricalPlayersRefactoringKata.Application.Interfaces
{
    public interface IGenreCalculator
    {
        decimal CalculateAmount(Performance perf, Play play);
        int CalculateVolumeCredits(Performance perf);
    }
}
