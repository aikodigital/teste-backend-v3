namespace TheatricalPlayersRefactoringKata.Models;

public interface IPlay
{
    string Name { get; }
    int Lines { get; }
    decimal CalculateAmount(int audience);
    int CalculateVolumeCredits(int audience);
}
