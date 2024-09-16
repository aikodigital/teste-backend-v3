using TheatricalPlayersRefactoringKata.Entities;

namespace TheatricalPlayersRefactoringKata.Services.PlayAmount;

public interface IPlayAmountService
{
    decimal GetAmount(PlayEntity play, int audience);
}