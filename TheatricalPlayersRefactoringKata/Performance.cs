namespace TheatricalPlayersRefactoringKata;

public class Performance
{
    private string _playId;
    private int _audience;

    public string PlayId { get => _playId; private set => _playId = value; }
    public int Audience { get => _audience; private set => _audience = value; }

    public Performance(string playID, int audience)
    {
        PlayId = playID;
        Audience = audience;
    }

}
