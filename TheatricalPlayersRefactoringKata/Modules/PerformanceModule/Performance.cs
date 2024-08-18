namespace TheatricalPlayersRefactoringKata.Modules;

public class Performance
{
    private string _playId;
    private int _audience;
    private PerformanceResults? _results;

    public string PlayId { get => _playId; set => _playId = value; }
    public int Audience { get => _audience; set => _audience = value; }
    public PerformanceResults? Results { get => _results; set => _results = value; }

    public Performance(string playID, int audience)
    {
        this._playId = playID;
        this._audience = audience;
    }

    public Performance(string playID, int audience, PerformanceResults results)
    {
        this._playId = playID;
        this._audience = audience;
        this._results = results;
    }
}