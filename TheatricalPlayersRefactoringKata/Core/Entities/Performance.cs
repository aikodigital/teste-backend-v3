#region

using TheatricalPlayersRefactoringKata.Core.Interfaces;

#endregion

namespace TheatricalPlayersRefactoringKata.Core.Entities;

public class Performance
{
    public Performance(IPlay play, int audience)
    {
        Play     = play;
        Audience = audience;
        Amount = Play.CalculateAmount(Audience);
    }

    public IPlay Play { get; set; }

    public int Audience { get; set; }

    public int Amount { get; }
}