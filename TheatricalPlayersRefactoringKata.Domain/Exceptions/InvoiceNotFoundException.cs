using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheatricalPlayersRefactoringKata.Domain.Exceptions
{
    public class InvoiceNotFoundException : Exception
    {
        public InvoiceNotFoundException(string invoiceId) : base($"Invoice with id {invoiceId} not found")
        {
        }
    }
}
