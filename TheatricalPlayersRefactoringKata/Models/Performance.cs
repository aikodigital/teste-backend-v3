namespace TheatricalPlayersRefactoringKata.Models;

public class Performance
{

    private int _audience;
    private Play _play;


    public int Audience { get => _audience; set => _audience = value; }
    public Play Play { get => _play; set => _play = value; }

    public Performance(int audience, Play play)
    {

        _audience = audience;
        _play = play;

    }

}
