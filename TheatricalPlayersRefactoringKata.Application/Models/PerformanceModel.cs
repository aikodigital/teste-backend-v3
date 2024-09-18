namespace TheatricalPlayersRefactoringKata.Application.Models;

public class PerformanceModel
{
    private string _playId;
    private int _audience;

    public string PlayId { get => _playId; set => _playId = value; }
    public int Audience { get => _audience; set => _audience = value; }

    public PerformanceModel(string playID, int audience)
    {
        _playId = playID;
        _audience = audience;
    }

}
