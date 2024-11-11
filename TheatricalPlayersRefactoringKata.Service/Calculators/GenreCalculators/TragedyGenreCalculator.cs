using TheatricalPlayersRefactoringKata.Service.Calculators.Interface;

namespace TheatricalPlayersRefactoringKata.Service.Calculators;

public class TragedyGenreCalculator : IGenreCalculator
{
    public uint CalculateGenre(uint audience, uint lines)
    {
        uint amount = lines * 10; //base value

        if (audience > 30) 
        {
            amount += + 1000 * (audience - 30);
        }
        
        return amount;
    }

    public uint CalculateCredits(uint audience)
    {
        int volumeCredits = Math.Max((int)audience - 30, 0);
        return (uint)volumeCredits;
    }
}
