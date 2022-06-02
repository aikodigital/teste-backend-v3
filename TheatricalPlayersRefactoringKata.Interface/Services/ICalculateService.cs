using TheatricalPlayersRefactoringKata.Domain.Model.Enum;

namespace TheatricalPlayersRefactoringKata.Domain.Interface.Services
{
    public interface ICalculateService
    {
        decimal CalculateValueByType(PlayTypeEnum playTypeEnum, int audience);
        decimal CalculateCreditsByType(PlayTypeEnum playTypeEnum, int audience);
    }
}