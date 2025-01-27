using TheatricalPlayersRefactoringKata.Domain.Interfaces;
using TheatricalPlayersRefactoringKata.Domain.Models;

namespace TheatricalPlayersRefactoringKata.Domain.Calculators.Types
{
    public class HistoryCalculator : TypeGenericCalculator
    {
        private readonly ITypeGenericCalculator _comedyCalculator;
        private readonly ITypeGenericCalculator _tragedyCalculator;

        public HistoryCalculator()
        {
            _comedyCalculator = new ComedyCalculator();
            _tragedyCalculator = new TragedyCalculator();
        }

        public override double Calculate(Performance perf)
        {
            return _comedyCalculator.Calculate(perf) +
                   _tragedyCalculator.Calculate(perf);
        }
    }
}
