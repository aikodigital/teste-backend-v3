using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheatricalPlayersRefactoringKata.Categorys
{
    //classes with comedy type methods
    public class TypeComedy : IType
    {
        //specific calculation of the value of the comedy type
        public double Calculate(int audience, int lines)
        {
            return CalculateByType.Comedy(audience, lines);
        }

        public string Category()
        {
            return "comedy";
        }

        //specific calculation of the credit comedy type
        public int VolumeCredits(int audience)
        {
            int credits = 0;
            credits += Math.Max(audience - 30, 0);
            credits += (int)Math.Floor((decimal)audience / 5);
            return credits;
        }
    }
}
