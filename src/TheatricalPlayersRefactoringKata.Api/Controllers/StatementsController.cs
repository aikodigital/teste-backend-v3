using Microsoft.AspNetCore.Mvc;
using TheatricalPlayersRefactoringKata.Application.UseCases.Statements.Print;
using TheatricalPlayersRefactoringKata.Communication.Requests;
using TheatricalPlayersRefactoringKata.Communication.Responses;

namespace TheatricalPlayersRefactoringKata.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatementsController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(typeof(ResponsePrintStatementJson), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register([FromServices] IPrintStatementUseCase usecase, [FromBody] RequestPrintStatementJson request)
        {
            var response = await usecase.ExecuteAsync(request);
            return Created(string.Empty, response);
        }
    }
}
