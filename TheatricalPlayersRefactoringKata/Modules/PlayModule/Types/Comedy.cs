namespace TheatricalPlayersRefactoringKata.Modules
{
    public class Comedy : AbstractPlayType
    {
        override public decimal CalculateAmount(Performance performance, Play play)
        {
            int audience = performance.Audience;
            return base.CalculateAmount(performance, play) + (audience > 20 ? 100 + 5 * (audience - 20) : 0) + 3 * audience;
        }

        override public decimal CalculateCredit(Performance performance, Play play)
        {
            int audience = performance.Audience;
            return base.CalculateCredit(performance, play) + Math.Floor((decimal)audience / 5);
        }
    }
}