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

    public IPlay Play { get; }

    public int Audience { get; }

    public int Amount { get; }

    public int CalculateCredits()
    {
        var credits = Math.Max(Audience - 30, 0);
        if (Play.Type == "comedy") credits += (int)Math.Floor((decimal)Audience / 5);
        return credits;
    }
}