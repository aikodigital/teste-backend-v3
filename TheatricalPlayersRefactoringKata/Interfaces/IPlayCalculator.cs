using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheatricalPlayersRefactoringKata.Interfaces
{
    public interface IPlayCalculator
    {
        public decimal calculateAmount(Performance perf, decimal currentAmount);
    }
}
