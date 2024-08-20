using TheatricalPlayersRefactoringKata.Interfaces;

namespace TheatricalPlayersRefactoringKata.Strategies
{
    public class TragedyStrategy : IPlayTypeStrategy
    {
        public double Execute(double thisAmount, int audience)
        {
            var result = thisAmount;

            if (audience > 30)
            {
                result += 1000 * (audience - 30);
            }
            return result;
        }
    }
}
