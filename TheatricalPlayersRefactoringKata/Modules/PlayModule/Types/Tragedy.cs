namespace TheatricalPlayersRefactoringKata.Modules
{
    public class Tragedy : AbstractPlayType
    {
        override public decimal CalculateAmount(Performance performance, Play play)
        {
            int audience = performance.Audience;
            return base.CalculateAmount(performance, play) + (audience > 30 ? 10 * (audience - 30) : 0);
        }
    }
}