using System;
using System.Globalization;

namespace TheatricalPlayersRefactoringKata;

public class Performance
{
    private string _playId;
    private int _audience;
    private decimal _cost;
    private int _credits;
    public string PlayId { get => _playId; set => _playId = value; }
    public int Audience
    {
        get => _audience; set
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException("Audience cannot be negative.");
            _audience = value;
        }
    }
    public decimal Cost { get => _cost; set => _cost = value; }
    public string FormattedCost => Cost % 1 == 0 ? ((int)Cost).ToString() : Cost.ToString("0.##", CultureInfo.InvariantCulture);

    public int Credits { get => _credits; set => _credits = value; }
    public decimal BasePrice { get; set; }

    public int StatementId {  get; set; }

    public Play Play { get; set; }

    public Performance(string playID, int audience)
    {
        if (audience < 0)
            throw new ArgumentOutOfRangeException("Audience cannot be negative.");

        this._playId = playID;
        this._audience = audience;
    }

}
