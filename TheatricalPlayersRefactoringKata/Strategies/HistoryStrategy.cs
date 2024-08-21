
using TheatricalPlayersRefactoringKata.Interfaces;

namespace TheatricalPlayersRefactoringKata.Strategies
{
    public class HistoryStrategy : IPlayTypeStrategy
    {
        public double Execute(double thisAmount, int audience)
        {
            var result = thisAmount;

            result += (300 * audience) + thisAmount;

            if (audience > 20)
            {
                result += 10000 + (500 * (audience - 20));
            }

            if (audience > 30)
            {
                result += 1000 * (audience - 30);
            }


            return result;
        }
    }
}
