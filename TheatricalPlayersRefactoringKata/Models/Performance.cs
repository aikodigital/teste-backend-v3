namespace TheatricalPlayersRefactoringKata.Models;

public class Performance
{
    private string _playId;
    private int _audience;

    public string PlayId { get => _playId; set => _playId = value; }
    public int Audience { get => _audience; set => _audience = value; }
    public Play Play { get; set; }

    public Performance(string playID, int audience, Play play=null)
    {
        this._playId = playID;
        this._audience = audience;
        this.Play = play;
    }

    

}
