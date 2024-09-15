using TheatricalPlayersRefactoringKata.Domain.Entities;
using TheatricalPlayersRefactoringKata.Domain.Enums;
using TheatricalPlayersRefactoringKata.Domain.Interfaces;
using TheatricalPlayersRefactoringKata.Domain.Validation;
using TheatricalPlayersRefactoringKata.Domain.ValueObjects;

namespace TheatricalPlayersRefactoringKata.Domain;

public class Play : Entity
{
    private string _name;
    private int _lines;
    private PlayType _playType;
    private IPlayTypeStrategy _playTypeStrategy;

    public string Name { get => _name; set => _name = value; }
    public int Lines { get => _lines; set => _lines = value; }
    public string TypeName { get => _playType.Name; }

    public Play(string name, int lines, PlayTypeEnum playType)
    {
        ValidateDomain(name, lines, playType);
        _playType = new PlayType(playType);
        _playTypeStrategy = _playType.GetStrategies();
    }

    private void ValidateDomain(string name, int lines, PlayTypeEnum playType)
    {
        DomainExceptionValidation.When(string.IsNullOrWhiteSpace(name), "Invalid name. Name is required.");
        this._name = name;

        DomainExceptionValidation.When(lines < 0, "Invalid value. Lines must be equal or higher than 0.");
        this._lines = lines;
    }

    public decimal CalculateAmountValueByAudience(int audience)
    {
        int lines = Math.Clamp(this.Lines, 1000, 4000);
        int baseValue = lines * 10;
        decimal totalAmount = _playTypeStrategy.CalculateTotalAmountByAudience(baseValue, audience);
        return decimal.Round(totalAmount / 100, 2);
    }

    public int CalculateCredits(int audience)
    {
        return _playTypeStrategy.CalculateCreditsByAudience(audience);
    }
}
