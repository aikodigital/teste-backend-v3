using TheatricalPlayersRefactoringKata.Entities;

namespace TheatricalPlayersRefactoringKata.Services.PlayVolumeCredits;

public interface IPlayVolumeCreditsService
{
    int GetVolumeCredits(PlayEntity play, int audience);
}