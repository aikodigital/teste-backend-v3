namespace TheatricalPlayersRefactoringKata
{
    public class TragedyStrategy : IPlayStrategy
    {
        public double CalculateAmount(Performance performance, double amount, int lines)
        {
            amount = lines * 10;
            if (performance.Audience > 30)
            {
                amount += 1000 * (performance.Audience - 30);
            }
            return amount;
        }
    }
}
