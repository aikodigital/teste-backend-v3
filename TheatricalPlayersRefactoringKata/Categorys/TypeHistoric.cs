using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheatricalPlayersRefactoringKata.Categorys
{
    //classes with historic type methods
    public class TypeHistoric : IType
    {
        //specific calculation of the value of the historic type
        public double Calculate(int audience, int lines)
        {
            return CalculateByType.Tragedy(audience, lines) + CalculateByType.Comedy(audience, lines);
        }

        public string Category()
        {
            return "comedy";
        }

        //specific calculation of the credit historic type
        public int VolumeCredits(int audience)
        {
            int credits = 0;
            credits += Math.Max(audience - 30, 0);
            return credits;
        }
    }
}
