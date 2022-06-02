using Microsoft.AspNetCore.Mvc;
using TheatricalPlayersRefactoringKata.App.Interfaces;
using TheatricalPlayersRefactoringKata.App.Model.Request;
using TheatricalPlayersRefactoringKata.App.Model.Response;

namespace TheatricalPlayersRefactoringKata.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoiceApp _invoiceApp;

        public InvoiceController(IInvoiceApp invoiceApp)
        {
            _invoiceApp = invoiceApp;
        }

        [HttpPost]
        [Route("invoice")]
        public async Task<IActionResult> Invoice([FromBody] Request<NewInvoiceRequest> request)
        {
            Response<NewInvoiceResponse> response = await _invoiceApp.Invoice(request);
            if (response.HasErrors)
                return BadRequest(response);
            else
                return Ok(response);
        }
    }
}