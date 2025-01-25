using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Domain.Interfaces;
using TheatricalPlayersRefactoringKata.Domain.Models;

namespace TheatricalPlayersRefactoringKata.Domain.Calculators.Types
{
    public abstract class TypeCalculator : ITypeCalculator
    {
        public virtual double Calculate(Performance perf)
        {
            var lines = perf.Play.Lines;
            if (lines < 1000) lines = 1000;
            if (lines > 4000) lines = 4000;
            var amount = lines * 10;

            return amount;
        }

        public virtual double CalculateCredits(Performance perf)
        {
            return Math.Max(perf.Audience - 30, 0);
        }
    }
}
