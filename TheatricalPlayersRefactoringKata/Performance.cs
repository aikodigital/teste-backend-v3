namespace TheatricalPlayersRefactoringKata;
public class Performance
{
    public string PlayId { get; }
    public int Audience { get; }
    public Performance(string playID, int audience)
    {
        PlayId = playID;
        Audience = audience;
    }
}