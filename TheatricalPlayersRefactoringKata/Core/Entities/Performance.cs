using System;
using System.Collections.Generic;
using TheatricalPlayersRefactoringKata.Application.Services.Gender;
using TheatricalPlayersRefactoringKata.Core.Entities;
using TheatricalPlayersRefactoringKata.Core.Enuns;

namespace TheatricalPlayersRefactoringKata.Core.Entities;

public class Performance
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid PlayId { get; set; }
    public Play Play { get; set; }
    public int Audience { get; set; }
    public Invoice Invoice { get; set; }
    public int Amount { get; init; }

    public Performance(Guid playId, int audience, Play play)
    {
        PlayId = playId;
        Audience = audience;
        Play = play;
        Amount = CalculateAmount(play);
    }

    public Performance()
    {
    }

    private int CalculateAmount(Play play)
    {
        return play.Type switch
        {
            Genre.Comedy => ComedyCalculator.CalculateAmount(this, play),
            Genre.Tragedy => TragedyCalculator.CalculateAmount(this, play),
            Genre.Historical => HistoryCalculator.CalculateAmount(this, play),
            _ => throw new Exception("unknown genre")
        };
    }

    public int CalculateCredits()
    {
        var credits = Math.Max(Audience - 30, 0);
        if (Play.Type == Genre.Comedy) credits += (int)Math.Floor((decimal)Audience / 5);
        return credits;
    }

}
