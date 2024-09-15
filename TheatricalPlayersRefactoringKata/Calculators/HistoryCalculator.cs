using TheatricalPlayersRefactoringKata.Models;

public class HistoryCalculator : PlayCalculator
{
    public HistoryCalculator(Performance performance, Play play)
        : base(performance, play) { }

    public override decimal CalculateAmount(Performance performance)
    {
        // Peça histórica combina tragédia e comédia
        var tragedyCalculator = new TragedyCalculator(performance, this.play); 
        var comedyCalculator = new ComedyCalculator(performance, this.play);   
        return tragedyCalculator.CalculateAmount(performance) + comedyCalculator.CalculateAmount(performance);
    }
}
