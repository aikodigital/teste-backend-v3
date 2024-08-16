using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using TheatricalPlayersRefactoringKata.App.Validations.Invoices.Register;
using TheatricalPlayersRefactoringKata.App.Validations.Plays.Register;
using TheatricalPlayersRefactoringKata.Communication.Requests;
using TheatricalPlayersRefactoringKata.Communication.Responses;

namespace TheatricalPlayersAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PlayController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponsePlay), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseError), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register(
        [FromServices] IRegisterPlayValidation validation,
        [FromBody] RequestPlay request)
    {

        var response = await validation.Execute(request);

        return Created(string.Empty, response);

    }

}
