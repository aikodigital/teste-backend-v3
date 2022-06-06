using System;

namespace TheatricalPlayersRefactoringKata;

[Obsolete($"This class should not be directly used anymore, instead, use the better decoupled Contracts.IPerformance type")]
public class Performance
{
    private string _playId;
    private int _audience;

    public string PlayId { get => _playId; set => _playId = value; }
    public int Audience { get => _audience; set => _audience = value; }

    public Performance(string playID, int audience)
    {
        this._playId = playID;
        this._audience = audience;
    }
}
