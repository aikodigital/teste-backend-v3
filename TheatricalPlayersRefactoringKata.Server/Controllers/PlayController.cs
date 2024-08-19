using Microsoft.AspNetCore.Mvc;

using TheatricalPlayersRefactoringKata.Server.Database.Repositories;
using TheatricalPlayersRefactoringKata.Server.Database.Repositories.Entities.Play;

namespace TheatricalPlayersRefactoringKata.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlayController : ControllerBase
    {
        private readonly PlayRepository _playRepository;

        public PlayController(PlayRepository playRepository)
        {
            _playRepository = playRepository;
        }

        [HttpGet("{name}")]
        public async Task<IActionResult> Get(string name)
        {
            PlayEntity? play = await _playRepository.GetByTitle(name);
            if (play == null)
            {
                return NotFound();
            }

            return Ok(play);
        }
    }
}
