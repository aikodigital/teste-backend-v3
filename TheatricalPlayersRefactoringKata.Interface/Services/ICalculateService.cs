using TheatricalPlayersRefactoringKata.Domain.Model.Enum;

namespace TheatricalPlayersRefactoringKata.Domain.Interface.Services
{
    public interface ICalculateService
    {
        decimal CalculateBaseValue(int lines);

        decimal CalculateTragedyValue(int audience, int lines);

        decimal CalculateComedyValue(int audience, int lines);
        decimal CalculateHistoryValue(int audience, int lines);

        decimal CalculateValueByType(PlayTypeEnum playTypeEnum, int audience, int lines);

        int CalculateCreditsByType(PlayTypeEnum playTypeEnum, int audience);

        int CalculateBaseCredits(int audience);

        int CalculateBonusCreditsComedy(int audience);
    }
}