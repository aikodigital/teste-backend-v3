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
    [HttpGet]
    [ProducesResponseType(typeof(ResponsePlays), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetAllPlays([FromServices] IGetAllPlaysValidation validation)
    {
        var response = await validation.Execute();

        if (response.Plays.Count != 0)
            return Ok(response);

        return NoContent();
    }
    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(typeof(ResponsePlays), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseError), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(
          [FromServices] IGetPlayByIdValidation validation,
          [FromRoute] long id)
    {
        var response = await validation.Execute(id);

        return Ok(response);
    }

}
