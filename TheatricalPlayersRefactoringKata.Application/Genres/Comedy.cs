using TheatricalPlayersRefactoringKata.Domain.Interfaces;

namespace TheatricalPlayersRefactoringKata.Application.Genres;

public class Comedy : IPlay
{
    public decimal CalculateAmount(int lines, int audience)
    {
        decimal amount = (decimal)lines / 10;
        
        if (audience > 30)
        {
            amount += 10 * (audience - 30);
        }
        
        return amount;
    }

    public decimal CalculateCredits(int audience)
    {
        var volumeCredits = Math.Max(audience - 30, 0);
        volumeCredits += (int)Math.Floor((decimal)audience / 5);
        
        return volumeCredits;
    }
}