using System;

namespace TheatricalPlayersRefactoringKata.Models;

public class Performance
{
    public Guid Id { get; set; }
    public int PlayId { get; set; }
    public Play Play { get; set; }
    public int Audience { get; set; }

    public Performance(Play play, int audience)
    {
        Play = play is not null ? play : throw new ArgumentException("Play cannot be null");
        Audience = audience >= 0 ? audience : throw new ArgumentException("Audiance must be greater than 0");
    }

}
