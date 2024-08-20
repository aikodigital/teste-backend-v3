using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheatricalPlayersRefactoringKata.Domain.Core.Interfaces.IRepositories {
    public interface IStatementPrinterRepository {

        string Print(Invoice invoice, Dictionary<string, Play> plays);

    }
}
