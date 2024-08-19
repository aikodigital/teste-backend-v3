using TheatricalPlayersRefactoringKata.Core.Entities;

public class HistoricalPlayTypeService : IPlayTypeService
{
    private readonly TragedyPlayTypeService _tragedyService;
    private readonly ComedyPlayTypeService _comedyService;

    public HistoricalPlayTypeService()
    {
        _tragedyService = new TragedyPlayTypeService();
        _comedyService = new ComedyPlayTypeService();
    }

    public int CalculateAmount(Performance performance, Play play)
    {
        int tragedyAmount = _tragedyService.CalculateAmount(performance, play);
        int comedyAmount = _comedyService.CalculateAmount(performance, play);
        return tragedyAmount + comedyAmount;
    }

    public int CalculateVolumeCredits(Performance performance)
    {
        int tragedyCredits = _tragedyService.CalculateVolumeCredits(performance);
        int comedyCredits = _comedyService.CalculateVolumeCredits(performance);
        return tragedyCredits + comedyCredits;
    }
}
