using Microsoft.AspNetCore.Mvc;
using TheatricalPlayersRefactoringKata.Application.UseCases.Plays.Register;
using TheatricalPlayersRefactoringKata.Communication.Requests;
using TheatricalPlayersRefactoringKata.Communication.Responses;

namespace TheatricalPlayersRefactoringKata.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlaysController : ControllerBase
    {
        /// <summary>
        /// register the play.
        /// </summary>
        /// <param name="request">model of request.</param>
        /// <returns>datas of play registered.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(ResponseRegisterPlayJson), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register([FromServices] IRegisterPlayUseCase usecase, [FromBody] RequestPlayJson request)
        {
            var response = await usecase.Execute(request);
            return Created(string.Empty, response);
        }
    }
}
