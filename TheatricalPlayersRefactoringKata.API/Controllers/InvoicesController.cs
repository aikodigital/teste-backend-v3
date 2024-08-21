using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using TheatricalPlayersRefactoringKata.Application.DTOs.InvoiceDTOs;
using TheatricalPlayersRefactoringKata.Application.Interfaces;
using TheatricalPlayersRefactoringKata.Domain.Enums;

namespace TheatricalPlayersRefactoringKata.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoicesController : ControllerBase
    {
        private readonly IInvoiceService _invoiceService;

        public InvoicesController(IInvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
        }

        [HttpPost]
        [SwaggerOperation(
            Summary = "Creates a new invoice.",
            Description = "Creates a new invoice based on the provided data.")]
        public async Task<IActionResult> Create(InvoiceRequest invoiceRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var response = await _invoiceService.CreateInvoice(invoiceRequest);

            return response.Status == HttpStatusCode.OK
                ? Ok(response)
                : BadRequest(response);
        }

        [HttpPost("Statements")]
        [SwaggerOperation(
            Summary = "Generates a statement for the specified invoice.",
            Description = "Generates a statement in either XML or TXT format and returns it as a downloadable file.")]
        public async Task<IActionResult> GenerateStatement(Guid invoiceId, Formats format)
        {
            if (!ModelState.IsValid || !Enum.IsDefined(typeof(Formats), format))
                return BadRequest();

            var formattedContent = await _invoiceService.GenerateStatement(invoiceId, format);

            return File(formattedContent.Item1,
                        formattedContent.Item2, 
                        $"invoice_{invoiceId}.{formattedContent.Item3}");
        }

        [HttpGet]
        [SwaggerOperation(
            Summary = "Gets all invoices.",
            Description = "Retrieves a list of all invoices. The response includes details about each invoice, such as the customer and performances.")]
        public async Task<IActionResult> GetAll()
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var response = await _invoiceService.GetInvoices();

            return response.Status == HttpStatusCode.OK
                ? Ok(response)
                : BadRequest(response);
        }
    }
}