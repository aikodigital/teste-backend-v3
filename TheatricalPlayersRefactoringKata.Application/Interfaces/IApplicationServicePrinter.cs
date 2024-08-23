using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Application.DTO;
using TheatricalPlayersRefactoringKata.Domain.Common.Result;

namespace TheatricalPlayersRefactoringKata.Application.Interfaces {
    public interface IApplicationServicePrinter {
        Result<string> PrintText(InvoiceDTO invoiceDTO, Dictionary<string, PlayDTO> playsDTO);
    }
}
