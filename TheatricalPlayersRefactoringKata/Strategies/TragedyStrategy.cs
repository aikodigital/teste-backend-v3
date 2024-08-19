using TheatricalPlayersRefactoringKata.Interfaces;

namespace TheatricalPlayersRefactoringKata.Strategies
{
    public class TragedyStrategy : IPlayTypeStrategy
    {
        public int Execute(int thisAmount, int audience)
        {
            int result = thisAmount;

            if (audience > 30)
            {
                result += 1000 * (audience - 30);
            }
            return result;
        }
    }
}
