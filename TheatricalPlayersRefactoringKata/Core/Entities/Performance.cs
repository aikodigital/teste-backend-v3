namespace TheatricalPlayersRefactoringKata.Core.Entities;

public class Performance
{
    public Play Play { get; set; }
    public string PlayId { get; private set; }
    public int Audience { get; private set; }

    public Performance(string playId, int audience, Play play)
    {
        PlayId = playId;
        Audience = audience;
        Play = play;

    }
}
