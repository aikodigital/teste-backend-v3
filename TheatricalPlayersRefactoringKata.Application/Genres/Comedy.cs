using TheatricalPlayersRefactoringKata.Domain.Entity;
using TheatricalPlayersRefactoringKata.Domain.Enum;

namespace TheatricalPlayersRefactoringKata.Application.Genres;

public class Comedy : Play
{
    public Comedy(string name, int lines, EnumGenres type)
        : base(name, lines, type)
    {
    }
    
    public override decimal CalculateAmount(int audience)
    {
        decimal amount = (decimal)Lines / 10 + 3 * audience;
        if (audience > 20)
        {
            amount += 100 + 5 * (audience - 20);
        }
        return amount;
    }

    public override decimal CalculateCredits(int audience)
    {
        var volumeCredits = Math.Max(audience - 30, 0);
        volumeCredits += (int)Math.Floor((decimal)audience / 5);
        
        return volumeCredits;
    }
}