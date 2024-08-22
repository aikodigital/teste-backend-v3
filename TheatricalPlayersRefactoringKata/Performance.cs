using System;

namespace TheatricalPlayersRefactoringKata;

public class Performance
{
    private string _playId;
    private int _audience;

    public string PlayId { get => _playId; set => _playId = value; }
    public int Audience { get => _audience; set => _audience = value; }

public Performance(string playID, int audience)
{
    if (audience < 0) 
        throw new ArgumentOutOfRangeException(nameof(audience), "Audience cannot be negative");

    this._playId = playID;
    this._audience = audience;
}

}
