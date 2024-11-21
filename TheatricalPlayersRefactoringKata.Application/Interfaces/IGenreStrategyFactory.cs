using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Domain.Interfaces;

namespace TheatricalPlayersRefactoringKata.Application.Interfaces
{
    public interface IGenreStrategyFactory
    {
        IGenreStrategy Create(string genre);
    }
}
