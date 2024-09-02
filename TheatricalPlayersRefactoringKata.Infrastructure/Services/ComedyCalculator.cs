using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Entities;
using TheatricalPlayersRefactoringKata.Interfaces;

namespace TheatricalPlayersRefactoringKata.Infrastructure.Services
{
    public class ComedyCalculator : IPlayTypeCalculator
    {
        public decimal CalculateAmount(Play play, Performance performance)
        {
            var lines = play.Lines;
            if (lines < 1000) lines = 1000;
            if (lines > 4000) lines = 4000;

            var baseAmount = lines / 10.0m + 3 * performance.Audience;
            if (performance.Audience > 20)
            {
                baseAmount += 100 + 5 * (performance.Audience - 20);
            }
            return Math.Round(baseAmount, 2);
        }

        public int CalculateCredits(Performance performance)
        {
            int credits = Math.Max(performance.Audience - 30, 0);
            credits += (int)Math.Floor(performance.Audience / 5m);
            return credits;
        }
    }
}
