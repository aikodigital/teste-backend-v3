using TheatricalPlayersRefactoringKata;

namespace TheatricalPlayersRefactoringKata.Services.PlayType;

public interface IPlayCalculator
{
    decimal CalculateAmount(Play play, int audience);
    int CalculateCredits(Play play, int audience);
}
