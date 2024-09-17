using TheatricalPlayersRefactoringKata.Domain.Entities;
using TheatricalPlayersRefactoringKata.Domain.Interfaces;

namespace TheatricalPlayersRefactoringKata.Application.UseCases.Calculators
{
    public class HistoryCalculator : ITheatricalCalculator
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
