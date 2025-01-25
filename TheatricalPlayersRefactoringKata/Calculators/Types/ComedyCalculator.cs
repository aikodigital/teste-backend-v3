using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Models;

namespace TheatricalPlayersRefactoringKata.Calculators.Types
{
    public class ComedyCalculator : TypeCalculator
    {
        public override double Calculate(Performance perf)
        {
            var amount = base.Calculate(perf);
            if (perf.Audience > 20)
            {
                amount += 10000 + 500 * (perf.Audience - 20);
            }
            amount += 300 * perf.Audience;
            return amount;
        }

        public override double CalculateCredits(Performance perf)
        {
            var volumeCredits = base.CalculateCredits(perf);
            volumeCredits += (int)Math.Floor((decimal)perf.Audience / 5);
            return volumeCredits;
        }
    }
}
