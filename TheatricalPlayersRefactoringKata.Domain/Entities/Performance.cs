using TheatricalPlayersRefactoringKata.Domain.Entities;
using TheatricalPlayersRefactoringKata.Domain.Validation;

namespace TheatricalPlayersRefactoringKata.Domain;

public class Performance : Entity
{
    private Play _play;
    private int _audience;

    public Play Play { get => _play; set => _play = value; }
    public int Audience { get => _audience; set => _audience = value; }

    public Performance(Play play, int audience)
    {
        ValidateDomain(audience);
        this._play = play;
    }

    private void ValidateDomain(int audience)
    {
        DomainExceptionValidation.When(audience < 0, "Invalid value. Audience must be equal or higher than 0.");
        this._audience = audience;
    }

    public decimal GetPlayAmountValue()
    {
        return _play.CalculateAmountValueByAudience(_audience);
    }

    public int GetPlayCreditsValue()
    {
        return _play.CalculateCreditsByAudience(_audience);
    }
}
