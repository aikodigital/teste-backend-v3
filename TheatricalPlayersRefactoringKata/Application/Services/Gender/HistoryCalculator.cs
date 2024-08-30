using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Application.Services;
using TheatricalPlayersRefactoringKata.Application.Services.Gender;
using TheatricalPlayersRefactoringKata.Core.Entities;

public class HistoryCalculator : IPerformanceCalculator
{
    private readonly TragedyCalculator _tragedyCalculator = new TragedyCalculator();
    private readonly ComedyCalculator _comedyCalculator = new ComedyCalculator();

    public async Task<decimal> CalculateAmount(Performance performance, Play play)
    {
        decimal tragedyAmount = await _tragedyCalculator.CalculateAmount(performance, play);
        decimal comedyAmount = await _comedyCalculator.CalculateAmount(performance, play);
        return tragedyAmount + comedyAmount;
    }


    public async Task<int> CalculateVolumeCredits(Performance performance, Play play)
    {
        int tragedyCredits = await _tragedyCalculator.CalculateVolumeCredits(performance, play);
        int comedyCredits = await _comedyCalculator.CalculateVolumeCredits(performance, play);
        return tragedyCredits + comedyCredits;
    }

}
