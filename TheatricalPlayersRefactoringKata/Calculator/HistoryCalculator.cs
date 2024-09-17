using TheatricalPlayersRefactoringKata.Interface;
using TheatricalPlayersRefactoringKata.Model;

namespace TheatricalPlayersRefactoringKata.Calculator;
public class HistoryCalculator : IPlayCalculator
{
    private readonly IPlayCalculator _tragedyCalculator = new TragedyCalculator();
    private readonly IPlayCalculator _comedyCalculator = new ComedyCalculator();

    public decimal CalculateAmount(Performance performance, Play play) => _tragedyCalculator.CalculateAmount(performance, play) +
               _comedyCalculator.CalculateAmount(performance, play);

    public int CalculateVolumeCredits(Performance performance) => _tragedyCalculator.CalculateVolumeCredits(performance) +
               _comedyCalculator.CalculateVolumeCredits(performance);
}

