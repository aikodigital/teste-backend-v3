using Microsoft.AspNetCore.Mvc;
using TheatricalPlayersRefactoringKata.API.DTO;
using TheatricalPlayersRefactoringKata.Entities;
using TheatricalPlayersRefactoringKata.Interfaces.Repositories;

namespace TheatricalPlayersRefactoringKata.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlayController : Controller
    {
        private readonly IBaseRepository<Play> _playRepository;

        public PlayController(IBaseRepository<Play> context)
        {
            _playRepository = context;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] PlayDTO play)
        {
            var _play = new Play
            {
                Name = play.Name,
                Lines = play.Lines,
                Type = play.Type
            };

            await _playRepository.AddAsync(_play);
            return CreatedAtAction(nameof(Get), new { id = _play.Name }, _play);
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var plays = await _playRepository.GetAllAsync();
            return Ok(plays);
        }
    }
}
