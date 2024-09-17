using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Domain.Enum;

namespace TheatricalPlayersRefactoringKata.Application.Interfaces
{
    public interface IInvoicePrintFactory
    {
        IInvoicePrint GetPrintType(PrintType type);
        PrintType DeterminePrintType(string printTypeRequest);
    }
}
