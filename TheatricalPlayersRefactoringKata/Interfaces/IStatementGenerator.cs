using System.Collections.Generic;
using TheatricalPlayersRefactoringKata.Entities;

namespace TheatricalPlayersRefactoringKata.Interfaces
{
    public interface IStatementGenerator
    {
        string GenerateStatement(Invoice invoice, Dictionary<string, Play> plays);
    }
}
