namespace TheatricalPlayersRefactoringKata;

public class Performance
{
    private Play _play;
    private int _audience;

    public Play play { get => _play; set => _play = value; }
    public int Audience { get => _audience; set => _audience = value; }

    public Performance(Play play, int audience)
    {
        this._play = play;
        this._audience = audience;
    }

}
