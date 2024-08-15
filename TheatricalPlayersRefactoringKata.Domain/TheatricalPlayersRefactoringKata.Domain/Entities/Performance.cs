using TheatricalPlayersRefactoringKata.Communication.Enums;

namespace TheatricalPlayersRefactoringKata.Domain.Entities;

public class Performance
{
    private string _playId;
    private int _audience;

    public PlayTypes PlayId;
    public int Audience { get => _audience; set => _audience = value; }

    public Performance(string playID, int audience)
    {
        _playId = playID;
        _audience = audience;
    }

}
