using System.Xml.Linq;

namespace TheatricalPlayersRefactoringKata.Entities;

public class Performance
{
    private Play _play;
    private int _audience;

    public Play play { get => _play; set => _play = value; }
    public int Audience { get => _audience; set => _audience = value; }

    public Performance(Play play, int audience)
    {
        _play = play;
        _audience = audience;
    }
    public decimal CalculateValue()
    {
        return _play.CalculateValue(_audience);
    }
    public int CalculateCredits()
    {
        return _play.CalculateCredits(_audience);
    }
}
