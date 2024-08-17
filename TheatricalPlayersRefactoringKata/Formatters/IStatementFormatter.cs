using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Models;

namespace TheatricalPlayersRefactoringKata.Formatters
{
    internal interface IStatementFormatter
    {
        string Format(Invoice invoice, Dictionary<string, Play> plays);
    }
}
