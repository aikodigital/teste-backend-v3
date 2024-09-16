using TheatricalPlayersRefactoringKata.Application.Interfaces;
using TheatricalPlayersRefactoringKata.Domain.Entities;

namespace TheatricalPlayersRefactoringKata.Application.Services.Calculators
{
    public class ComedyCalculator : IGenreCalculator
    {
        public decimal CalculateAmount(Performance perf, Play play)
        {
            decimal thisAmount = 30000;
            if (perf.Audience > 20)
            {
                thisAmount += 10000 + 500 * (perf.Audience - 20);
            }
            thisAmount += 300 * perf.Audience;
            return thisAmount;
        }

        public int CalculateVolumeCredits(Performance perf)
        {
            int volumeCredits = Math.Max(perf.Audience - 30, 0);
            volumeCredits += (int)Math.Floor((decimal)perf.Audience / 5);  // Extra credits for comedy
            return volumeCredits;
        }
    }
}
