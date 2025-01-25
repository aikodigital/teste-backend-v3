using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Models;

namespace TheatricalPlayersRefactoringKata.Calculators.Types
{
    public class TragedyCalculator : TypeCalculator
    {
        public override double Calculate(Performance perf)
        {
            var amount = base.Calculate(perf);

            if (perf.Audience > 30)
            {
                amount += 1000 * (perf.Audience - 30);
            }

            return amount;
        }

    }
}
