using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Domain.Common.Result;

namespace TheatricalPlayersRefactoringKata.Domain.Core.Interfaces.IRepositories {
    public interface IStatementPrinterRepository {

        Result<string> Print(Invoice invoice, Dictionary<string, Play> plays);

    }
}
