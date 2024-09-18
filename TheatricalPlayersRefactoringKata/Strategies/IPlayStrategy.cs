using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Models;

namespace TheatricalPlayersRefactoringKata.Strategies
{
    public interface IPlayStrategy
    {
        int Calculate(Performance performance, Play play);
    }

}
