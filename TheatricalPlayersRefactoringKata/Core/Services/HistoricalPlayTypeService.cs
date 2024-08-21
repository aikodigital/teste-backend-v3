using TheatricalPlayersRefactoringKata.Core.Entities;

namespace TheatricalPlayersRefactoringKata.Core.Services.PlayTypeServices;

public class HistoricalPlayTypeService : IPlayTypeService
{
    private readonly TragedyPlayTypeService _tragedyService = new TragedyPlayTypeService();
    private readonly ComedyPlayTypeService _comedyService = new ComedyPlayTypeService();

    public int CalculateAmount(Performance performance, Play play)
    {
        var tragedyAmount = _tragedyService.CalculateAmount(performance, play);
        var comedyAmount = _comedyService.CalculateAmount(performance, play);
        return tragedyAmount + comedyAmount;
    }

    public int CalculateVolumeCredits(Performance performance)
    {
        var tragedyCredits = _tragedyService.CalculateVolumeCredits(performance);
        var comedyCredits = _comedyService.CalculateVolumeCredits(performance);
        return tragedyCredits + comedyCredits;
    }
}

