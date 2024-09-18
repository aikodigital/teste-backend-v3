using TheatricalPlayersRefactoringKata.Domain.Entities;

namespace TheatricalPlayersRefactoringKata.Domain.Interfaces;

public interface IPlayTypeCalculator
{
    public decimal CalculateAmount(Performance performance);
    public int CalculateVolumeCredits(Performance performance);
}
