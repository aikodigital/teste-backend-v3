using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheatricalPlayersRefactoringKata
{
    public class TypeHistoric : IType
    {
        public double Calculate(int audience, int lines)
        {
            return CalculateByType.Tragedy(audience,lines) + CalculateByType.Comedy(audience, lines);
        }

        public string Category()
        {
            return "comedy";
        }

        public int VolumeCredits(int audience)
        {
            int credits = 0;
            credits += Math.Max(audience - 30, 0);
            return credits;
        }
    }
}
