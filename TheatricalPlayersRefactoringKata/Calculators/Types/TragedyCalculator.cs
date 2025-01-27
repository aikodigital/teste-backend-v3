using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Domain.Models;

namespace TheatricalPlayersRefactoringKata.Domain.Calculators.Types
{
    public class TragedyCalculator : TypeGenericCalculator
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
