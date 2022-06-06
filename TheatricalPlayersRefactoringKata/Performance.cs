namespace TheatricalPlayersRefactoringKata;

public class Performance
{
    private int _audience;

    public readonly Play _play;

    public int Audience => _audience;

    public decimal Amount => _play.BaseValue;

    public string PlayName => _play.Name;

    public Performance(Play play, int audience)
    {
        _play = play;
        _audience = audience;
    }

    public int GetCredits()
    {
        return _play.GetCredits(_audience);
    }

    public void CalculteAmount()
    {
        _play.CalculateBaseValue(_audience);
    }
}
