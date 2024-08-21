namespace TheatricalPlayersRefactoringKata.Domain.Entity;

public class Performance
{
    public string PlayId { get; private set; }
    public int Audience { get; private set; }

    public Performance(string playID, int audience)
    {
        PlayId = playID;
        Audience = audience;
    }
}
