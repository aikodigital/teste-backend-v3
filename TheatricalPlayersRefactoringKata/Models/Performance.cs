namespace TheatricalPlayersRefactoringKata.Models;

public class Performance
{
    private Play _play;
    private int _audience;

    public Play Play { get => _play; set => _play = value; }
    public int Audience { get => _audience; set => _audience = value; }

    public Performance(Play play, int audience)
    {
        _play = play;
        _audience = audience;
    }

}
