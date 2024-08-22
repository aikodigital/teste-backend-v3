using TheatricalPlayersRefactoringKata.Domain.Entity;
using TheatricalPlayersRefactoringKata.Domain.Enum;

namespace TheatricalPlayersRefactoringKata.Application.Genres;

public class Tragedy : Domain.Entity.Play
{
    public Tragedy(string name, int lines)
        : base(name, lines)
    {
        Type = EnumGenres.Tragedy;
    }
    
    public override decimal CalculateAmount(int audience)
    {
        decimal amount = (decimal)Lines / 10;
        if (audience > 30)
        {
            amount += 10 * (audience - 30);
        }
        return amount;
    }

    public override decimal CalculateCredits(int audience)
    {
        return Math.Max(audience - 30, 0);
    }
}