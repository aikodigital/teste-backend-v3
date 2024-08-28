using TheatricalPlayersRefactoringKata.Entities;

namespace TheatricalPlayersRefactoringKata.Interfaces
{
    public interface IPlayTypeCalculatorStrategy
    {
        double CalculateAmount(Play play, Performance performance);
        int CalculateCredit(Performance performance);
    }
}
