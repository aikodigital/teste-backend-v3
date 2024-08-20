using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheatricalPlayersRefactoringKata
{
    abstract class CalculateByType
    {
        public static double Tragedy(int audience, int lines)
        {
            double baseV = lines / 10.0;
            if (audience <= 30)
            {
                return baseV;
            }
            return baseV + ((audience - 30) * 10.0);
        }

        public static double Comedy(int audience, int lines)
        {
            double baseV = (audience * 3) + (lines / 10.0);

            if (audience > 20)
            {
                return baseV + 100 + ((audience - 20) * 5.00);
            }
            return baseV;
        }
    }
}
