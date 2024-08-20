using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheatricalPlayersRefactoringKata.Types
{
    public class Comedy : Play
    {
        public Comedy(string name, int lines) : base(name, lines) { }

        /// <summary>
        /// Calculates the total value based on the audience.
        /// Add an additional amount of 3.00 per person and apply an extra adjustment 
        /// if the audience exceeds 20 people. The extra adjustment consists of a fixed amount of 100.00 
        /// plus an additional amount of 5.00 per person over 20.
        /// </summary>
        /// <returns>
        /// The total calculated value, including audience-based extras.
        /// </returns>
        public override double CalculatorValor()
        {
            double valorBase = CalculatorValorBase();
            double valueWithAdditionals = valorBase + (Audience * 3.00);

            if (Audience > 20)
            {
                valueWithAdditionals += 100.00 + (Audience - 20) * 5.00;
            }

            return valueWithAdditionals;
        }

        /// <summary>
        /// Calculates credits based on audience.
        /// Includes additional credits if the audience exceeds 30 people.
        /// </summary>
        /// <returns>
        /// The total number of credits calculated based on audience.
        /// Includes base credits and bonus credits if applicable.
        /// </returns>
        public override int CalculatorCredits()
        {
            int baseCredits = Audience <= 30 ? 0 : (Audience - 30);
            int bonusCredits = Audience > 30 ? Audience / 5 : 0;
            return baseCredits + bonusCredits;
        }
    }
}


