using System;

namespace TheatricalPlayersRefactoringKata;

public class Performance
{
    public Guid Guid { get; set; }

    private int _audience;

    private readonly Play _play;

    public int Audience => _audience;

    public decimal Amount => _play.BaseValue;

    public string PlayName => _play.Name;

    public Performance(Play play, int audience)
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
