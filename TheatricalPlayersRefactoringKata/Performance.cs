using System;

namespace TheatricalPlayersRefactoringKata;

//Apresentação de teatro
public class Performance
{
    private int _audience;
    
    public Play Play { get; private set; }

    public int Audience => _audience;

    public int Amount => Play.BaseValue;

    public Performance(Play play, int audience)
    {
        Play = play;
        _audience = audience;
    }

    public int GetCredits()
    {
        return Play.GetCredits(_audience);
    }
}
