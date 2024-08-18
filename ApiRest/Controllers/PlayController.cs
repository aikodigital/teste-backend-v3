using Microsoft.AspNetCore.Mvc;
using TheatricalPlayersRefactoringKata.Models;
using TheatricalPlayersRefactoringKata.DTOs;
using TheatricalPlayersRefactoringKata.API.Repositories.Interfaces;

namespace TheatricalPlayersRefactoringKata.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PlayController : ControllerBase
    {
        private readonly IPlayRepository _playRepository;

        public PlayController(IPlayRepository playRepository)
        {
            _playRepository = playRepository;
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreatePlay([FromBody] PlayDto playDto)
        {
            if (playDto == null)
            {
                return BadRequest("Play data is null.");
            }

            var play = new Play(playDto.Name, playDto.Lines, playDto.Type);

            await _playRepository.AddAsync(play);

            return CreatedAtAction(nameof(GetPlay), new { id = play.Name }, play);
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPlay(int id)
        {
            var play = await _playRepository.GetByIdAsync(id);
            if (play == null)
            {
                return NotFound();
            }

            return Ok(play);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPlays()
        {
            var plays = await _playRepository.GetAllAsync();
            return Ok(plays);
        }
    }
}
