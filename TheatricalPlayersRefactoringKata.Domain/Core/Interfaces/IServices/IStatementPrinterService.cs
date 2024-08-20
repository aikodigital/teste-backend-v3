using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheatricalPlayersRefactoringKata.Domain.Core.Interfaces.IServices {
    public interface IStatementPrinterService {

        string Print(Invoice invoice, Dictionary<string, Play> plays);


    }
}
