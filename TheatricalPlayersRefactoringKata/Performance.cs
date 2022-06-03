using System;

namespace TheatricalPlayersRefactoringKata;

//Apresentação de teatro
public class Performance
{
    private const int COMEDY_AUDIENCE_DIVISION_CREDIT = 5;

    private const int AUDIENCE_FROM_AT_LEAST_FOR_CREDIT = 30;

    private int _audience;
    
    public Play Play { get; private set; }

    public int Audience => _audience;

    public Performance(Play play, int audience)
    {
        Play = play;
        _audience = audience;
    }

    public int GetCredits()
    {
        var volumeCredits = GetCreditForCommedyAudience();
        volumeCredits += GetCreditsForAudience();

        return volumeCredits;
    }

    private int GetCreditForCommedyAudience()
    {
        if (Play is not ComedyPlay) return 0;

        return (int)Math.Floor((decimal)Audience / COMEDY_AUDIENCE_DIVISION_CREDIT);
    }

    private int GetCreditsForAudience()
    {
        return Math.Max(Audience - AUDIENCE_FROM_AT_LEAST_FOR_CREDIT, 0);
    }

}
