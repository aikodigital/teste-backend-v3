using System.Collections.Generic;
using TheatricalPlayersRefactoringKata.Core.Entities;

namespace TheatricalPlayersRefactoringKata.Core.Interfaces
{
    public interface IStatementGenerator
    {
        string Generate(Invoice invoice, Dictionary<string, Play> plays);
    }
}