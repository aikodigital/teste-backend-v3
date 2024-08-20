using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheatricalPlayersRefactoringKata
{
    public class TypeComedy : IType
    {
        public double Calculate(int audience, int lines)
        {
            return CalculateByType.Comedy(audience, lines);
        }

        public string Category()
        {
            return "comedy";
        }

        public int VolumeCredits(int audience)
        {
            int credits = 0;
            credits += Math.Max(audience - 30, 0);
            credits += (int)Math.Floor((decimal)audience / 5);
            return credits;
        }
    }
}
