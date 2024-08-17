using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TheatricalPlayersRefactoringKata.infra;
using TheatricalPlayersRefactoringKata.Models;

namespace TheatricalPlayersRefactoringKata.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PlayController : ControllerBase
    {
        private ApiDbContext _db;

        public PlayController(ApiDbContext db)
        {
            _db = db;
        }

        [HttpPost]
        [Route("create")]
        public IActionResult CreatePlay([FromBody] Play play)
        {
            if (play == null)
            {
                return BadRequest("Play data is null.");
            }
            _db.Plays.Add(play);
            _db.SaveChanges();

            return CreatedAtAction(nameof(GetPlay), new { id = play.Id }, play);
        }
        [HttpGet("{id}")]
        public IActionResult GetPlay(string id)
        {
            var play = _db.Plays.Find(id);
            if (play == null)
            {
                return NotFound();
            }

            return Ok(play);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllPlays()
        {
            var plays = await _db.Plays.ToListAsync();
            return Ok(plays);
        }
    }
}

