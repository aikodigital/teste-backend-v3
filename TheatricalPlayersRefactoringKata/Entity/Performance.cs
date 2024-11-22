using System;

namespace TheatricalPlayersRefactoringKata.Entity;

public class Performance
{
    public Performance() {}

    private string _playId;
    private int _audience;

    public Guid Id { get; set; }

    public string PlayId { get => _playId; set => _playId = value; }
    public int Audience { get => _audience; set => _audience = value; }

    public Performance(string playID, int audience)
    {
        _playId = playID;
        _audience = audience;
    }

}
