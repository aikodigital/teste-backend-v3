using TheatricalPlayersRefactoringKata.Application.Interfaces;
using TheatricalPlayersRefactoringKata.Domain.Entities;

namespace TheatricalPlayersRefactoringKata.Application.Services.Calculators
{
    public class HistoryCalculator : IGenreCalculator
    {
        private readonly TragedyCalculator _tragedyCalculator;
        private readonly ComedyCalculator _comedyCalculator;

        public HistoryCalculator()
        {
            _tragedyCalculator = new TragedyCalculator();
            _comedyCalculator = new ComedyCalculator();
        }

        public decimal CalculateAmount(Performance perf, Play play)
        {
            return _tragedyCalculator.CalculateAmount(perf, play) + _comedyCalculator.CalculateAmount(perf, play);
        }
        public int CalculateVolumeCredits(Performance perf)
        {
            return Math.Max(perf.Audience - 30, 0);
        }
    }
}
