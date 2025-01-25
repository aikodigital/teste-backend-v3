using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Calculators.Interfaces;
using TheatricalPlayersRefactoringKata.Models;

namespace TheatricalPlayersRefactoringKata.Calculators.Types
{
    public class HistoryCalculator : TypeCalculator
    {
        private readonly ITypeCalculator _comedyCalculator;
        private readonly ITypeCalculator _tragedyCalculator;

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
