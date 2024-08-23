using Microsoft.AspNetCore.Mvc;
using TheatricalPlayersRefactoringKata.Application.DTO;
using TheatricalPlayersRefactoringKata.Application.Interfaces;
using TheatricalPlayersRefactoringKata.Domain.Common.Result;
using TheatricalPlayersRefactoringKata.Domain.Enums;

namespace TheatricalPlayersRefactoringKata.API.Controllers {
    [Route("v1/[controller]")]
    [ApiController]
    public class StatementPrinterController : ControllerBase {

        private readonly IApplicationServicePrinter _applicationService;

        public StatementPrinterController(IApplicationServicePrinter ApplicationService) {
            _applicationService = ApplicationService;
        }

        [HttpGet(Name = "print_statement")]
        public Result<IActionResult> PrintStatement([FromBody] InvoiceDTO invoice, Dictionary<string, PlayDTO> plays) {

            if (invoice == null || invoice.Performances == null) {
                return Result<IActionResult>.Failure(Error.Validation("The invoice data is invalid", ErrorType.Validation.ToString()), BadRequest());
            }

            if (plays == null) {
                return Result<IActionResult>.Failure(Error.Validation("Any play data is invalid", ErrorType.Validation.ToString()), BadRequest());
            }

            Result<string> invoiceString = _applicationService.PrintText(invoice, plays);

            return Result<IActionResult>.Success(Ok(invoiceString.Value));
        }
    }
}
