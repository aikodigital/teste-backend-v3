using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Core.Entities;

namespace TheatricalPlayersRefactoringKata.Application.Services.Gender;

public class HistoryCalculator : IPerformanceCalculator
{
    private readonly TragedyCalculator _tragedyCalculator = new();
    private readonly ComedyCalculator _comedyCalculator = new();

    public async Task<int> CalculateAmount(Performance performance, Play play)
    {
        var tragedyAmount = await _tragedyCalculator.CalculateAmount(performance, play);
        var comedyAmount = await _comedyCalculator.CalculateAmount(performance, play);
        return tragedyAmount + comedyAmount;
    }

    public async Task<int> CalculateVolumeCredits(Performance performance, Play play)
    {
        var tragedyCredit = await _tragedyCalculator.CalculateVolumeCredits(performance, play);
        var comedyCredit = await _comedyCalculator.CalculateVolumeCredits(performance, play);
        return tragedyCredit + comedyCredit;
    }
}
