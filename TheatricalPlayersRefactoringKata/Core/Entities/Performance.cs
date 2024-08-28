namespace TheatricalPlayersRefactoringKata.Core.Entities;

public class Performance
{
    public string PlayId { get; private set; }
    public int Audience { get; private set; }

    public Performance(string playId, int audience)
    {
        PlayId = playId;
        Audience = audience;
    }
}
