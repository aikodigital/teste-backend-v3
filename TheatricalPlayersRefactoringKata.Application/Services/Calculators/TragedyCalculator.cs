using TheatricalPlayersRefactoringKata.Application.Interfaces;
using TheatricalPlayersRefactoringKata.Domain.Entities;

namespace TheatricalPlayersRefactoringKata.Application.Services.Calculators
{
    public class TragedyCalculator : IGenreCalculator
    {
        public decimal CalculateAmount(Performance perf, Play play)
        {
            var lines = play.Lines;
            if (lines < 1000) lines = 1000;
            if (lines > 4000) lines = 4000;
            var thisAmount = lines * 10;

            if (perf.Audience > 30)
            {
                thisAmount += 1000 * (perf.Audience - 30);
            }
            return thisAmount;
        }

        public int CalculateVolumeCredits(Performance perf)
        {
            return Math.Max(perf.Audience - 30, 0);
        }
    }
}
