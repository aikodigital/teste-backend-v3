using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TheatricalPlayersRefactoringKata.infra;
using TheatricalPlayersRefactoringKata.Models;
using TheatricalPlayersRefactoringKata.Services;

namespace TheatricalPlayersRefactoringKata.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TheatricalPlayersRefactoringKataController : ControllerBase
    {
        private ApiDbContext _db;
        private readonly StatementPrinter _statementPrinter;

        public TheatricalPlayersRefactoringKataController(ApiDbContext db, StatementPrinter statementPrinter)
        {
            _db = db;
            _statementPrinter = statementPrinter;
        }

        [HttpPost]
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
        public IActionResult GetPlay(int id)
        {
            var play = _db.Plays.Find(id);
            if (play == null)
            {
                return NotFound();
            }

            return Ok(play);
        }
    }
}
