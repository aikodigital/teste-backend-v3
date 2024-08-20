namespace TheatricalPlayersRefactoringKata.entities;

public class Performance(string playId, int audience)
{
    public string PlayId { get; set; } = playId;

    public int Audience { get; set; } = audience;
}