using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheatricalPlayersRefactoringKata.Categorys
{
    public interface IType
    {
        public double Calculate(int audience, int lines);
        public string Category();
        public int VolumeCredits(int audience);
    }
}