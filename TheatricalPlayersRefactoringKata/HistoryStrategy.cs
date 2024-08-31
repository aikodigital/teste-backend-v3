
namespace TheatricalPlayersRefactoringKata
{
    public class HistoryStrategy : IPlayStrategy
    {
        public double CalculateAmount(Performance performance, double amount, int lines)
        {
            amount = (lines * 10)*2;
            if (performance.Audience > 30)
            {
                amount += 1000 * (performance.Audience - 30);
            }

            //amount =+ lines * 10;
            if (performance.Audience > 20)
            {
                amount += 10000 + 500 * (performance.Audience - 20);
                amount += 300 * performance.Audience;
            }
            else
            {
                amount += 300 * performance.Audience;
            }
            return amount;
        }
    }
}
