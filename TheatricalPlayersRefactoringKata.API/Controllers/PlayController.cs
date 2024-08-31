using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TheatricalPlayersRefactoringKata.Entities;
using TheatricalPlayersRefactoringKata.Infrastructure.Persistence;

namespace TheatricalPlayersRefactoringKata.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlayController : Controller
    {
        private static List<Play> plays = new List<Play>();
        private readonly DBContext _dbContext;

        public PlayController(DBContext context)
        {
            _dbContext = context;
        }

        [HttpPost]
        public async Task<ActionResult<List<Play>>> Post([FromBody] Play play)
        {
            //_dbContext.Plays.Add(play);
            //await _dbContext.SaveChangesAsync();
            //return CreatedAtAction(nameof(Get), new { play }, play);
            
            plays.Add(play);
            return plays;
        }

        [HttpGet]
        public ActionResult<List<Play>> Get() => 
            plays;
    }
}
