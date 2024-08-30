
namespace TheatricalPlayersRefactoringKata
{
    public class ComedyStrategy : IPlayStrategy
    {
        public double CalculateAmount(Performance performance, double amount, int lines)
        {
            amount = lines * 10;
            if (performance.Audience > 20)
            {
                amount += 10000 + 500 * (performance.Audience - 20);
                amount += 300 * performance.Audience;
                return amount;
            } else {
                amount += 300 * performance.Audience;
                return amount;
            }
        }
    }
}
