using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Models;
using TheatricalPlayersRefactoringKata.Services;

namespace TheatricalPlayersRefactoringKata.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoiceService _invoiceService;

        public InvoiceController(IInvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
        }

        [HttpPost("generate/xml")]
        public async Task<IActionResult> GenerateXmlAsync([FromBody] InvoiceRequest invoiceRequest)
        {
            var filePath = await _invoiceService.GenerateInvoiceXmlAsync(invoiceRequest);
            return Ok(new { FilePath = filePath });
        }
    }
}
