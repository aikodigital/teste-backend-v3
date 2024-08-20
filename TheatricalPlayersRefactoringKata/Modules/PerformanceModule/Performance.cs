namespace TheatricalPlayersRefactoringKata.Modules;

public class Performance
{
    private string _playId;
    private int _audience;
    private PerformanceResults? _results;

    public string PlayId { get => _playId; set => _playId = value; }
    public int Audience { get => _audience; set => _audience = value; }
    public PerformanceResults? Results { get => _results; set => _results = value; }

    public Performance(string playId, int audience)
    {
        _playId = playId;
        _audience = audience;
    }

    public Performance(string playId, int audience, PerformanceResults results)
    {
        _playId = playId;
        _audience = audience;
        _results = results;
    }
}