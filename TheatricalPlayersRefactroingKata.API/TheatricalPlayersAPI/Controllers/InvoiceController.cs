using Microsoft.AspNetCore.Mvc;
using TheatricalPlayersRefactoringKata.App.Validations.Invoices.Register;
using TheatricalPlayersRefactoringKata.Communication.Requests;
using TheatricalPlayersRefactoringKata.Communication.Responses;

namespace TheatricalPlayersAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class InvoiceController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseInvoice), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseError), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register(
        [FromServices] IRegisterInvoiceValidation validation,
        [FromBody] RequestInvoice request)
    {

        var response = await validation.Execute(request);

        return Created(string.Empty, response);

    }

    [HttpGet]
    [ProducesResponseType(typeof(ResponseInvoices), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetAllInvoices([FromServices] IGetAllInvoiceValidation validation)
    {
        var response = await validation.Execute();

        if (response.Invoices.Count != 0)
            return Ok(response);

        return NoContent();
    }

    [HttpGet]
    [Route("{name}")]
    [ProducesResponseType(typeof(ResponseInvoice), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseError), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByCustomer(
      [FromServices] IGetInvoiceByCustomerValidation validation,
      [FromRoute] string name)
    {
        var response = await validation.Execute(name);

        return Ok(response);
    }
}
