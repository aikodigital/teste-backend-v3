using TheatricalPlayersRefactoringKata.Domain.Interfaces;

namespace TheatricalPlayersRefactoringKata.Application.Genres;

public class History : IPlay
{
    private readonly Tragedy _tragedy;
    private readonly Comedy _comedy;

    public History()
    {
        _tragedy = new Tragedy();
        _comedy = new Comedy();
    }
    
    public decimal CalculateAmount(int lines, int audience)
    {
        decimal tragedyPrice = _tragedy.CalculateAmount(audience, lines);
        decimal comedyPrice = _comedy.CalculateAmount(audience, lines);
        return tragedyPrice + comedyPrice;
    }

    public decimal CalculateCredits(int audience)
    {
        decimal tragedyCredits = _tragedy.CalculateCredits(audience);
        decimal comedyCredits = _comedy.CalculateCredits(audience);
        return tragedyCredits + comedyCredits;
    }
}