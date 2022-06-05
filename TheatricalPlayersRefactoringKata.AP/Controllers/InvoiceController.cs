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

        [HttpGet]
        [Route("extract/invoice-xml/{invoiceId}")]
        public async Task<IActionResult> ExtractXML([FromRoute] long invoiceId)
        {
            Response<string> response = await _invoiceApp.GenerateExtract(invoiceId, Domain.Model.Enum.ExtractTypeEnum.XML);
            if (response.HasErrors)
                return BadRequest(response.ErrorMessage);
            else
                return Ok(response.Value);
        }

        [HttpGet]
        [Route("extract/invoice-text/{invoiceId}")]
        public async Task<IActionResult> ExtractText([FromRoute] long invoiceId)
        {
            Response<string> response = await _invoiceApp.GenerateExtract(invoiceId, Domain.Model.Enum.ExtractTypeEnum.Text);
            if (response.HasErrors)
                return BadRequest(response.ErrorMessage);
            else
                return Ok(response.Value);
        }

        [HttpGet]
        [Route("extract/invoice-json/{invoiceId}")]
        public async Task<IActionResult> ExtractJson([FromRoute] long invoiceId)
        {
            Response<string> response = await _invoiceApp.GenerateExtract(invoiceId, Domain.Model.Enum.ExtractTypeEnum.Json);
            if (response.HasErrors)
                return BadRequest(response.ErrorMessage);
            else
                return Ok(response.Value);
        }
    }
}