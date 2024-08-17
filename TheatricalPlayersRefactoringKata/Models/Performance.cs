
namespace TheatricalPlayersRefactoringKata.Models;
public class Performance(Play play, int audience)
{

    private Play _play = play;
    private int _audience = audience;

    public Play Play { get => _play; set => _play = value; }
    public int Audience { get => _audience; set => _audience = value; }

    public int Charge {
        get => _play.CalculateCharge(_audience);
    }

    public int Credits {
        get => _play.CalculateCredits(_audience);
    }
}
