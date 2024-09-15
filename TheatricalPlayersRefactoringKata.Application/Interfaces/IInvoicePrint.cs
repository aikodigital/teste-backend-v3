using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Domain.Entities;

namespace TheatricalPlayersRefactoringKata.Application.Interfaces
{
    public interface IInvoicePrint
    {
        string Print(InvoicePrint.Statement invoicePrint);
    }
}
