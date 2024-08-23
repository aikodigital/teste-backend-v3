using System;
using System.Collections.Generic;
using TheatricalPlayersRefactoringKata.Domain.Common.Result;


namespace TheatricalPlayersRefactoringKata.Domain.Core.Interfaces.IServices {
    public interface IStatementPrinterService {

        Result<string> PrintText(Invoice invoice, Dictionary<string, Play> plays);
    }
}
