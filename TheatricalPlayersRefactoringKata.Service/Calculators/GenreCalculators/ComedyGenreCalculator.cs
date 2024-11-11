using TheatricalPlayersRefactoringKata.Service.Calculators.Interface;

namespace TheatricalPlayersRefactoringKata.Service.Calculators;

public class ComedyGenreCalculator : IGenreCalculator
{
    public uint CalculateGenre(uint audience, uint lines)
    {
        uint amount = lines * 10; //base value

        if (audience > 20) 
        {
            amount += 10000 + 500 * (audience - 20);
        }
        
        amount += 300 * audience;
        return amount;
    }

    public uint CalculateCredits(uint audience)
    {
        int volumeCredits = Math.Max((int)audience - 30, 0);
        // add extra credit for every ten comedy attendees
        volumeCredits += (int)Math.Floor((decimal)audience / 5);

        return (uint)volumeCredits;
    }
}
