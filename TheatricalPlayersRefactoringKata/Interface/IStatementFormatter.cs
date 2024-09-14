using System.Collections.Generic;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Model;

namespace TheatricalPlayersRefactoringKata.Interface;
public interface IStatementFormatter
{
    Task<string> FormatAsync(Invoice invoice, Dictionary<string, Play> plays);
}

