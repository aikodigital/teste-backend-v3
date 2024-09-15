using TheatricalPlayersRefactoringKata.Domain.Interfaces;

namespace TheatricalPlayersRefactoringKata.Domain.Strategies
{
    internal class TragedyPlayTypeStrategies : IPlayTypeStrategy
    {
        public int CalculateTotalAmountByAudience(int baseValue, int audience)
        {
            int totalAmount = baseValue;

            if (audience > 30)
            {
                totalAmount += 1000 * (audience - 30);
            }

            return totalAmount;
        }

        public int CalculateCreditsByAudience(int valueBase, int audience)
        {
            return valueBase;
        }
    }
}
