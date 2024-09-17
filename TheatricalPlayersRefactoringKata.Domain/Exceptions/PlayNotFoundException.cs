using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheatricalPlayersRefactoringKata.Domain.Exceptions
{
    public class PlayNotFoundException : Exception
    {
        public PlayNotFoundException(string invoiceId) : base($"Play not found: {invoiceId}")
        {
        }
    }
}
