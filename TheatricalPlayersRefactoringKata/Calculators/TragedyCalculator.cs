using TheatricalPlayersRefactoringKata.Models;

public class TragedyCalculator : PlayCalculator
{
    public TragedyCalculator(Performance performance, Play play)
        : base(performance, play) { }

    public override decimal CalculateAmount(Performance performance)
    {
        decimal amount = 40000;
        if (performance.Audience > 30)
        {
            amount += 1000 * (performance.Audience - 30);
        }
        return amount;
    }
}
