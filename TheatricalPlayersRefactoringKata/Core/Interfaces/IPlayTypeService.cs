using TheatricalPlayersRefactoringKata.Core.Entities;

public interface IPlayTypeService
{
    int CalculateAmount(Performance performance, Play play);
    int CalculateVolumeCredits(Performance performance);
}
