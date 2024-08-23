using TheatricalPlayersRefactoringKata.Core.Interfaces;
using TheatricalPlayersRefactoringKata.Core.Services;
using TheatricalPlayersRefactoringKata.Core.Services.Calculators;

namespace TheatricalPlayersRefactoringKata.Core.Entities;

public class Performance : IPerformance
{
    public Performance(Play play, int audience, Guid id)
    {
        Play = play;
        PlayId = play.Id;
        Audience = audience;
        Id = id;
        Amount = CalculateAmount(play);
    }
    
    public Performance()
    {
    }

    private int CalculateAmount(Play? play)
    {
        return play!.Type switch
        {
            Genre.Comedy => ComedyCalculator.CalculateAmount(this, play),
            Genre.Tragedy => TragedyCalculator.CalculateAmount(this, play),
            Genre.Historical => HistoricalCalculator.CalculateAmount(this, play),
            _ => throw new Exception("unknown genre")
        };
    }

    public int CalculateCredits()
    {
        var credits = Math.Max(Audience - 30, 0);
        if (Play.Type == Genre.Comedy) credits += (int)Math.Floor((decimal)Audience / 5);
        return credits;
    }

    public Guid Id { get; }
    public Guid PlayId { get; init; }
    public Play Play { get; set; } = new();
    public int Audience { get; init; } = 0;
    public int Amount { get; init; }
    public List<Invoice> Invoices { get; init; } = [];
}