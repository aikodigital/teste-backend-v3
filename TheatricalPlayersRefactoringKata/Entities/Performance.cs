namespace TheatricalPlayersRefactoringKata.Entities;

public class Performance
{
    private string _playId;
    private int _audience;

    public string PlayId { get => _playId; set => _playId = value; }
    public int Audience { get => _audience; set => _audience = value; }

    public Performance(string playID, int audience)
    {
        _playId = playID;
        _audience = audience;
    }

}
