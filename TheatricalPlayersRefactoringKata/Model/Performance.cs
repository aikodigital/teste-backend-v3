namespace TheatricalPlayersRefactoringKata.Model;

public class Performance
{
    public string PlayId { get; set; }
    public int Audience { get; set; }

    public Performance()
    {
        
    }
    public Performance(string playID, int audience)
    {
        PlayId = playID;
        Audience = audience;
    }

}
