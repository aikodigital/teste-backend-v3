using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheatricalPlayersRefactoringKata.Domain.Exceptions
{
    public class PerformanceNotFoundException : Exception
    {
        public PerformanceNotFoundException(string invoiceId) : base($"Performance with invoiceId {invoiceId} not found")
        {
        }
    }
}
