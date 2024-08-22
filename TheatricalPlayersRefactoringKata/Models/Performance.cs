using System;

namespace TheatricalPlayersRefactoringKata.Models;

public class Performance
{
    private Play _play;
    private int _audience;

    public Play Play { get => _play; set => _play = value; }
    public int Audience { get => _audience; set => _audience = value; }

    public Performance(Play play, int audience)
    {
        _play = play is not null ? play : throw new ArgumentException("Play cannot be null");
        _audience = audience >= 0 ? audience : throw new ArgumentException("Audiance should be greater than 0");
    }

}
