namespace TheatricalPlayersRefactoringKata
{
    public class HistoryAmountCalculator : IPlayAmountCalculator
    {
        public int CalculateAmount(Performance perf, int baseAmount)
        {
            var amount = 0;
            amount += new TragedyAmountCalculator().CalculateAmount(perf, baseAmount);
            amount += new ComedyAmountCalculator().CalculateAmount(perf, baseAmount);
            return amount;
        }
    }
}
