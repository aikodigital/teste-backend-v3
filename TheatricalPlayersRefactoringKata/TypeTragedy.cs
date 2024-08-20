using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TheatricalPlayersRefactoringKata
{
    public class TypeTragedy : IType
    {
        public double Calculate(int audience, int lines)
        {
            return CalculateByType.Tragedy(audience, lines);
        }

        public string Category()
        {
            return "comedy";
        }

        public int VolumeCredits(int audience)
        {

            return Math.Max(audience - 30, 0);

        }
    }
}