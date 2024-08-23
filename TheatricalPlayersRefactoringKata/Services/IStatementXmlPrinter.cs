using System.Collections.Generic;
using TheatricalPlayersRefactoringKata.Models;

namespace TheatricalPlayersRefactoringKata.Services
{
    public interface IStatementXmlPrinter
    {
        string PrintXml(Invoice invoice, Dictionary<string, Play> plays);
    }
}
