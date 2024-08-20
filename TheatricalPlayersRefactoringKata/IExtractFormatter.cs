using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheatricalPlayersRefactoringKata
{
    public interface IExtractFormatter
    {
        public string Formatter(Invoice invoice, Dictionary<string, Play> plays);
    }
}
