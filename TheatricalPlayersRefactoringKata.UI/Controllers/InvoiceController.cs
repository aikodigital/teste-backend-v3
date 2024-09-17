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
        private readonly IInvoiceRepository _invoiceRepository;
        StatementPrinter _StatementPrinter;

        public InvoiceController(ICalculateBaseAmountPerLine calculateBaseAmountPerLine,
                                ICalculateCreditAudience calculateCreditAudience,
                                ICalculateAdditionalValuePerPlayType calculateAdditionalValuePerPlayType,
                                IInvoicePrintFactory invoicePrintFactory,
                                IInvoiceRepository invoiceRepository)
        {
            _calculateBaseAmountPerLine = calculateBaseAmountPerLine;
            _calculateCreditAudience = calculateCreditAudience;
            _calculateAdditionalValuePerPlayType = calculateAdditionalValuePerPlayType;
            _invoicePrintFactory = invoicePrintFactory;
            _invoiceRepository = invoiceRepository;
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
            try
            {
                StatementPrinter _StatementPrinter = new StatementPrinter(_calculateBaseAmountPerLine,
                                                                          _calculateCreditAudience,
                                                                          _calculateAdditionalValuePerPlayType,
                                                                          _invoicePrintFactory,
                                                                          _invoiceRepository);
                return _StatementPrinter.PrintInvoice(invoiceId, printTypeRequest);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }            
        }
    }
}
