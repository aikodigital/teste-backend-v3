namespace TheatricalPlayersRefactoringKata.Entities;

public interface ICalculator
{
    int CalculateAmount(Performance performance, Play play);
    int CalculateCredits(Performance performance, Play play);
}
