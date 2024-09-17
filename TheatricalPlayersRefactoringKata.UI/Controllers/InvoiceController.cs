using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Annotations;
using TheatricalPlayersRefactoringKata.Application;
using TheatricalPlayersRefactoringKata.Application.Interfaces;

namespace TheatricalPlayersRefactoringKata.UI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InvoiceController : Controller
    {
        private readonly ICalculateBaseAmountPerLine _calculateBaseAmountPerLine;
        private readonly ICalculateCreditAudience _calculateCreditAudience;
        private readonly ICalculateAdditionalValuePerPlayType _calculateAdditionalValuePerPlayType;
        private readonly IInvoicePrintFactory _invoicePrintFactory;
        StatementPrinter _StatementPrinter;

        public InvoiceController(ICalculateBaseAmountPerLine calculateBaseAmountPerLine,
                                ICalculateCreditAudience calculateCreditAudience,
                                ICalculateAdditionalValuePerPlayType calculateAdditionalValuePerPlayType,
                                IInvoicePrintFactory invoicePrintFactory)
        {
            _calculateBaseAmountPerLine = calculateBaseAmountPerLine;
            _calculateCreditAudience = calculateCreditAudience;
            _calculateAdditionalValuePerPlayType = calculateAdditionalValuePerPlayType;
            _invoicePrintFactory = invoicePrintFactory;
        }

        /// <summary>
        /// Gets a list of weather forecasts.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /weatherforecast
        ///
        /// </remarks>
        /// <returns>List of weather forecasts</returns>
        /// <response code="200">Returns the list of weather forecasts</response>
        /// <response code="500">If there is an internal server error</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public string Index(string invoiceId, string printTypeRequest)
        {
            StatementPrinter _StatementPrinter = new StatementPrinter(_calculateBaseAmountPerLine,
                                                                      _calculateCreditAudience,
                                                                      _calculateAdditionalValuePerPlayType,
                                                                      _invoicePrintFactory);
            return _StatementPrinter.PrintInvoice(invoiceId, printTypeRequest);
        }
    }
}
