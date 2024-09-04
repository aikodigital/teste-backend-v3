using AutoMapper;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TheatricalPlayersRefactoringKata.Dtos;
using TheatricalPlayersRefactoringKata.Entities;
using TheatricalPlayersRefactoringKata.Services.Interface;

namespace TheatricalPlayersRefactoringKata.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoiceService _invoiceService;
        private readonly IMapper _mapper;

        // Constructor injecting the Invoice service and AutoMapper
        public InvoiceController(IInvoiceService invoiceService, IMapper mapper)
        {
            _invoiceService = invoiceService;
            _mapper = mapper;
        }

        // POST: Create a new invoice
        [HttpPost("CreateInvoice")]
        public async Task<IActionResult> CreateInvoice([FromBody] InvoiceDto invoiceDto)
        {
            // Map DTO to Invoice entity
            var invoice = _mapper.Map<Invoice>(invoiceDto);

            // Call service to create the invoice
            var createdInvoice = await _invoiceService.CreateInvoiceAsync(invoice);

            // Map the created invoice back to DTO
            var createdInvoiceDto = _mapper.Map<InvoiceDto>(createdInvoice);

            // Return created response with the new invoice details
            return CreatedAtAction(nameof(GetInvoiceById), new { id = createdInvoice.Id }, createdInvoiceDto);
        }

        // GET: Retrieve an invoice by its ID
        [HttpGet("GetInvoiceById/{id}")]
        public async Task<IActionResult> GetInvoiceById(int id)
        {
            // Get the invoice by ID through the service
            var invoice = await _invoiceService.GetInvoiceByIdAsync(id);

            // Map the invoice entity to DTO for the response
            var invoiceDto = _mapper.Map<InvoiceDto>(invoice);

            // Return OK with the found invoice details
            return Ok(invoiceDto);
        }

        // PUT: Update an existing invoice
        [HttpPut("UpdateInvoice/{id}")]
        public async Task<IActionResult> UpdateInvoice(int id, [FromBody] InvoiceDto invoiceDto)
        {
            // Map DTO to Invoice entity
            var invoice = _mapper.Map<Invoice>(invoiceDto);
            invoice.Id = id;

            // Call the service to update the invoice
            await _invoiceService.UpdateInvoiceAsync(invoice);

            // Return NoContent to indicate success
            return NoContent();
        }

        // DELETE: Delete an invoice by its ID
        [HttpDelete("DeleteInvoice/{id}")]
        public async Task<IActionResult> DeleteInvoice(int id)
        {
            // Call the service to delete the invoice by ID
            await _invoiceService.DeleteInvoiceAsync(id);

            // Return NoContent to indicate success
            return NoContent();
        }
    }
}
