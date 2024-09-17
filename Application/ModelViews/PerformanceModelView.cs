namespace TheatricalPlayersRefactoringKata;

public class PerformanceModelView
{
    private string _playId;
    private int _audience;

    public string PlayId { get => _playId; set => _playId = value; }
    public int Audience { get => _audience; set => _audience = value; }

    public PerformanceModelView(string playID, int audience)
    {
        this._playId = playID;
        this._audience = audience;
    }

}
