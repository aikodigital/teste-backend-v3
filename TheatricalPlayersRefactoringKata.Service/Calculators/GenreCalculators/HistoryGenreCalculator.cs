using TheatricalPlayersRefactoringKata.Service.Calculators.Interface;

namespace TheatricalPlayersRefactoringKata.Service.Calculators;

public class HistoryGenreCalculator : IGenreCalculator
{
    public uint CalculateGenre(uint audience, uint lines)
    {
        TragedyGenreCalculator tragedyGenreCalculator = new TragedyGenreCalculator();
        ComedyGenreCalculator comedyGenreCalculator = new ComedyGenreCalculator();

        uint tragedyValue = tragedyGenreCalculator.CalculateGenre(audience, lines);
        uint comedyValue = comedyGenreCalculator.CalculateGenre(audience, lines);

        return tragedyValue + comedyValue;
    }
    
    public uint CalculateCredits(uint audience)
    {
        int volumeCredits = Math.Max((int)audience - 30, 0);
        return (uint)volumeCredits;
    }
    
}
