

namespace TheatricalPlayersRefactoringKata.Domain.Interfaces
{
    public interface IPlayTypeStrategy
    {
        public int CalculateTotalAmountByAudience(int baseValue, int audience);
        public int CalculateCreditsByAudience(int audience);
    }
}
