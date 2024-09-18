using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Annotations;
using TheatricalPlayersRefactoringKata.Application;
using TheatricalPlayersRefactoringKata.Application.Interfaces;

namespace TheatricalPlayersRefactoringKata.UI.Controllers
{
    /// <summary>
    /// Controller for handling invoice-related API requests.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class InvoiceController : Controller
    {
        private readonly ICalculateBaseAmountPerLine _calculateBaseAmountPerLine;
        private readonly ICalculateCreditAudience _calculateCreditAudience;
        private readonly ICalculateAdditionalValuePerPlayType _calculateAdditionalValuePerPlayType;
        private readonly IInvoicePrintFactory _invoicePrintFactory;
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly StatementPrinter _statementPrinter;

        /// <summary>
        /// Initializes a new instance of the <see cref="InvoiceController"/> class.
        /// </summary>
        /// <param name="calculateBaseAmountPerLine">Service for calculating the base amount per line.</param>
        /// <param name="calculateCreditAudience">Service for calculating credits based on audience.</param>
        /// <param name="calculateAdditionalValuePerPlayType">Service for calculating additional value per play type.</param>
        /// <param name="invoicePrintFactory">Factory for creating invoice print types.</param>
        /// <param name="invoiceRepository">Repository for accessing invoice data.</param>
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
            _statementPrinter = new StatementPrinter(_calculateBaseAmountPerLine,
                                                     _calculateCreditAudience,
                                                     _calculateAdditionalValuePerPlayType,
                                                     _invoicePrintFactory,
                                                     _invoiceRepository);
        }

        /// <summary>
        /// Retrieves and prints an invoice based on the provided invoice ID and print type request.
        /// </summary>
        /// <param name="invoiceId">The ID of the invoice to print.</param>
        /// <param name="printTypeRequest">The requested print type (e.g., XML, text).</param>
        /// <returns>A string representation of the invoice in the requested format.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public string Index(string invoiceId, string printTypeRequest)
        {
            try
            {
                return _statementPrinter.PrintInvoice(invoiceId, printTypeRequest);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
