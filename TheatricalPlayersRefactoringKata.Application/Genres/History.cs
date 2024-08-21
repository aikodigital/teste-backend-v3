using TheatricalPlayersRefactoringKata.Domain.Entity;
using TheatricalPlayersRefactoringKata.Domain.Enum;

namespace TheatricalPlayersRefactoringKata.Application.Genres;

public class History : Play
{
    private readonly Tragedy _tragedy;
    private readonly Comedy _comedy;

    public History(string name, int lines, EnumGenres type)
        : base(name, lines, type)
    {
        _tragedy = new Tragedy(name, lines, type);
        _comedy = new Comedy(name, lines, type);
    }
    
    public override decimal CalculateAmount(int audience)
    {
        decimal tragedyPrice = _tragedy.CalculateAmount(audience);
        decimal comedyPrice = _comedy.CalculateAmount(audience);
        return tragedyPrice + comedyPrice;
    }

    public override decimal CalculateCredits(int audience)
    {
        return Math.Max(audience - 30, 0);
    }
}