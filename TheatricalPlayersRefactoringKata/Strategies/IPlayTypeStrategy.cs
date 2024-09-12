using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheatricalPlayersRefactoringKata.Strategies
{
    public interface IPlayTypeStrategy
    {
        decimal CalculateAmount(int lines, int audience);
        int CalculateVolumeCredits(int audience);
    }
}
