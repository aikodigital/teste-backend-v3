using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheatricalPlayersRefactoringKata.Interfaces
{
   public interface IStatementStrategy
    {
    public string StatementFormat(Invoice invoice, Dictionary<string, Play> plays);
    }
}