using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheatricalPlayersRefactoringKata.Application.Interfaces
{
    public interface IPlayService
    {
        Task<Domain.Entities.Play> CreatePlayAsync(Domain.Entities.Play play);
    }
}
