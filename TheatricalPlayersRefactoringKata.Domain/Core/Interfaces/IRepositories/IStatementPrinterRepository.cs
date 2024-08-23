using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Domain.Common.Result;

namespace TheatricalPlayersRefactoringKata.Domain.Core.Interfaces.IRepositories {
    public interface IStatementPrinterRepository {

        Result<string> PrintText(Invoice invoice, Dictionary<string, Play> plays, Dictionary<Enum, IGenreStrategy> genres);

    }
}
