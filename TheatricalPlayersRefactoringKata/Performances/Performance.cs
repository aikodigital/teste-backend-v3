using System;

namespace TheatricalPlayersRefactoringKata.Performances;

public class Performance
{
    private readonly int _audience;

    private readonly IPlay _play;

    public Guid Guid { get; private set; }

    public int Audience => _audience;

    public decimal Amount => _play.BaseValue;

    public string PlayName => _play.Name;

    public Performance(IPlay play, int audience)
    {
        _play = play;
        _audience = audience;
        Guid = Guid.NewGuid();
    }

    public int GetCredits()
    {
        return _play.GetCredits(_audience);
    }

    public void CalculateAmount()
    {
        _play.CalculateBaseValue(_audience);
    }

}
