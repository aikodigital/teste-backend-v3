using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Communication.Request;
using TheatricalPlayersRefactoringKata.Communication.Response;

namespace TheatricalPlayersRefactoringKata.Application.UseCases.Extract;

public interface IExtractInvoiceUseCase
{
    Task<ExtractResponse> ExecuteAsync(ExtractRequest request);
}
