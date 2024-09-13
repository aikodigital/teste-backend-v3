namespace TheatricalPlayersRefactoringKata
{
    public class ComedyAmountCalculator : IPlayAmountCalculator
    {
        public int CalculateAmount(Performance perf, int baseAmount)
        {
            if (perf.Audience > 20)
            {
                baseAmount += 10000 + 500 * (perf.Audience - 20);
            }
            baseAmount += 300 * perf.Audience;

            return baseAmount;
        }
    }
}
