using Application.Invoices.Commands.CreateInvoice;
using Application.Invoices.Commands.DeleteInvoice;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ApiControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<int>> Create(CreateInvoiceCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteInvoiceCommand { Id = id });

            return NoContent();
        }
    }
}
