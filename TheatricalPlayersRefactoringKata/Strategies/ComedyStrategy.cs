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
        public int Execute(int thisAmount, int audience)
        {
            int result = thisAmount;
            if (audience > 20)
            {
                result += 10000 + 500 * (audience - 20);
            }
            result += 300 * audience;
            return result;
        }
    }
}
