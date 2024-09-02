using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Entities;
using TheatricalPlayersRefactoringKata.Interfaces;

namespace TheatricalPlayersRefactoringKata.Infrastructure.Services
{
    public class TragedyCalculator : IPlayTypeCalculator
    {
        public decimal CalculateAmount(Play play, Performance performance)
        {
            var lines = play.Lines;
            if (lines < 1000) lines = 1000;
            if (lines > 4000) lines = 4000;

            var baseAmount = lines / 10.0m;
         
            if (performance.Audience > 30)
            {
                baseAmount += 10 * (performance.Audience - 30);
            }
            return baseAmount;
        }

        public int CalculateCredits(Performance performance)
        {
            return Math.Max(performance.Audience - 30, 0);
        }
    }
}
