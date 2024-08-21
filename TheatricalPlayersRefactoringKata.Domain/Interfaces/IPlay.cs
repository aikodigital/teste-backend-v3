namespace TheatricalPlayersRefactoringKata.Domain.Interfaces;

public interface IPlay
{
    decimal CalculateAmount(int lines, int audience);
    decimal CalculateCredits(int audience);
}