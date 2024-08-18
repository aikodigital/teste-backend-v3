namespace TheatricalPlayersRefactoringKata.Models;

public class Performance
{
    private string _playId;
    private int _audience;
    private Play _play;

    public string PlayId { get => _playId; set => _playId = value; }
    public int Audience { get => _audience; set => _audience = value; }
    public Play Play { get => _play; set => _play = value; }

    public Performance(string playID, int audience, Play play)
    {
        _playId = playID;
        _audience = audience;
        _play = play;

    }

}
