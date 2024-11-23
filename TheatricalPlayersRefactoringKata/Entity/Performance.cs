using System;

namespace TheatricalPlayersRefactoringKata.Entity;

public class Performance
{
    public Performance() {}

    public Guid Id { get; set; }

    public Play Play { get; set; }

    public int Audience { get; set; }
}
