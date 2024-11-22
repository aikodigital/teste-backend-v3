using Aplication.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Entity;

namespace Aplication.Interfaces
{
    public interface IPlayService
    {
        List<PlayDto> GetPlays();
    }
}
