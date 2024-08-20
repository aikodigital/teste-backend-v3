using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TheatricalPlayersRefactoringKata.Categorys
{
    //classes with tragedy type methods
    public class TypeTragedy : IType
    {
        //specific calculation of the value of the tragedy type
        public double Calculate(int audience, int lines)
        {
            return CalculateByType.Tragedy(audience, lines);
        }

        public string Category()
        {
            return "comedy";
        }

        //specific calculation of the credit tragedy type
        public int VolumeCredits(int audience)
        {

            return Math.Max(audience - 30, 0);

        }
    }
}