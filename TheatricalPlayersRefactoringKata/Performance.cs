namespace TheatricalPlayersRefactoringKata;

public class Performance
{
    private string _playId;
    private int _audience;
    private decimal _cost;
    private int _credits;

    public string PlayId { get => _playId; set => _playId = value; }
    public int Audience { get => _audience; set => _audience = value; }
    public decimal Cost { get => _cost; set => _cost = value; }
    public int Credits { get => _credits; set => _credits = value; }

    public Play Play { get; set; }

    public Performance(string playID, int audience)
    {
        this._playId = playID;
        this._audience = audience;
    }

}
