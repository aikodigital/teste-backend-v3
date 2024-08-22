#region

using TheatricalPlayersRefactoringKata.Core.Services;
using TheatricalPlayersRefactoringKata.Core.Services.Calculators;

#endregion

namespace TheatricalPlayersRefactoringKata.Core.Entities;

public class Performance
{
    public Performance(Play? play, int audience, List<Invoice> invoices , Guid id)
    {
        Play = play;
        PlayId = play!.Id;
        Audience = audience;
        Invoices = invoices;
        Id = id;
        Amount = CalculateAmount(play);
    }
    
    public Performance(){}
    
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
    
    public Guid Id { get; }
    public Guid PlayId { get; }
    public Play? Play { get; set; }

    public int Audience { get; }

    public int Amount { get; set; }
    public List<Invoice>? Invoices { get; init; }

    public int CalculateCredits()
    {
        var credits = Math.Max(Audience - 30, 0);
        if (Play!.Type == Genre.Comedy) credits += (int)Math.Floor((decimal)Audience / 5);
        return credits;
    }
}