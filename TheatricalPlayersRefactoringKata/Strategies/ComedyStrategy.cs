using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Interfaces;

namespace TheatricalPlayersRefactoringKata.Strategies
{
    public class ComedyStrategy : IPlayTypeStrategy
    {
        public double Execute(double thisAmount, int audience)
        {
            var result = thisAmount;
            result += 300 * audience;
            if (audience > 20)
            {
                result += 10000 + (500 * (audience - 20));
            }
            
            return result;
        }
    }
}
