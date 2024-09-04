using System.Collections.Generic;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Entities;

namespace TheatricalPlayersRefactoringKata.Services.Interface
{
    public interface IStatementPrinterService
    {
        Task<string> Print(Invoice invoice, Dictionary<string, Play> plays);
        Task<string> PrintAsXml(Invoice invoice, Dictionary<string, Play> plays);
    }
}
