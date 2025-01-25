namespace TheatricalPlayersRefactoringKata.Domain.Models;

public class Performance
{
    private string _playName;
    private int _audience;

    public string PlayName { get => _playName; set => _playName = value; }
    public int Audience { get => _audience; set => _audience = value; }
    public Play Play { get; set; }

    public Performance(string playName, int audience, Play play=null)
    {
        this._playName = playName;
        this._audience = audience;
        this.Play = play;
    }
}
