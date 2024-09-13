namespace TheatricalPlayersRefactoringKata.Entities;

public class PerformanceEntity
{
    public string PlayId { get; private set; }

    public int Audience { get; private set; }

    public PerformanceEntity(string playId, int audience)
    {
        PlayId = playId;
        Audience = audience;
    }
}
