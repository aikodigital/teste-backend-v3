using System;
using TheatricalPlayersRefactoringKata.Entities;
using TheatricalPlayersRefactoringKata.Enum;

namespace TheatricalPlayersRefactoringKata.Services.PlayVolumeCredits;

public class PlayVolumeCreditsService : IPlayVolumeCreditsService
{
    public int GetVolumeCredits(PlayEntity play, int audience)
    {
        var volumeCredits = Math.Max(audience - 30, 0);

        if (play.Type == PlayTypeEnum.Comedy)
        {
            volumeCredits += (int)Math.Floor((decimal)audience / 5);
        }

        return volumeCredits;
    }
}