using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheatricalPlayersRefactoringKata.Interfaces
{
    public interface ICalculateStrategy
    {
        public decimal CalculateAmount(Play play, Performance performance);
        public int CalculateCredits(Play play, Performance performance);
    }

}