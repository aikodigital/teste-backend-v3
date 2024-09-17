using TheatricalPlayersRefactoringKata.Model;

namespace TheatricalPlayersRefactoringKata.Interface;

public interface IPlayCalculator
{
    decimal CalculateAmount(Performance performance, Play play);
    int CalculateVolumeCredits(Performance performance);
}
