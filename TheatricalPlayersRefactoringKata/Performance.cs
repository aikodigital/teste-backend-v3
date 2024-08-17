namespace TheatricalPlayersRefactoringKata;

public class Performance
{
    private string _playId;
    private int _audience;
    private string _playGenre;

    public string PlayId { get => _playId; set => _playId = value; }
    public int Audience { get => _audience; set => _audience = value; }
    public string PlayGenre { get => _playGenre; set => _playGenre = value; }

    public Performance(string playID, int audience, string playGenre)
    {
        this._playId = playID;
        this._audience = audience;
        this._playGenre = playGenre;
    }

}
