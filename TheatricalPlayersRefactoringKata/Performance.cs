namespace TheatricalPlayersRefactoringKata;
public class Performance
{
    public Play Play { get; private set; }
    public int Audience { get; private set; }

    public Performance(Play play, int audience)
    {
        Play = play;
        Audience = audience;
    }

    public decimal CalculateAmount()
    {
        return Play.CalculateAmount(Audience);
    }

    public int CalculateCredits()
    {
        return Play.CalculateCredits(Audience);
    }
}
