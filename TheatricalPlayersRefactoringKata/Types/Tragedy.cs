using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheatricalPlayersRefactoringKata.Types
{
    public class Tragedy : Play
    {
        public Tragedy(string name, int lines) : base(name, lines) { }

        /// <summary>
        /// Calculates the total value based on the audience.
        /// Adds an adjustment to the base value if the audience exceeds 30 people.
        /// </summary>
        /// <returns>
        /// The total calculated value, including an additional adjustment for audiences over 30 people.
        /// </returns>
        public override double CalculatorValor()
        {
            double valorBase = CalculatorValorBase();
            if (Audience <= 30)
            {
                return valorBase;
            }
            return valorBase  + (Audience - 30) * 10.00;
        }
    }
}
