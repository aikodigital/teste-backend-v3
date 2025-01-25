using Microsoft.AspNetCore.Mvc;

namespace TheatricalPlayersRefactoringKata.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TypeController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> MudarIssoAqui()
        {
            return Ok();
        }
    }
}
