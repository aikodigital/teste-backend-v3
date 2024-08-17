using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TheatricalPlayersRefactoringKata.infra;
using TheatricalPlayersRefactoringKata.Models;
using TheatricalPlayersRefactoringKata.DTOs;

namespace TheatricalPlayersRefactoringKata.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PerformanceController : ControllerBase
    {
        private ApiDbContext _db;

        public PerformanceController(ApiDbContext db)
        {
            _db = db;
        }

        [HttpPost]
        [Route("create")]
        public IActionResult CreatePerformance([FromBody] PerformanceDto performanceDto)
        {
            if (performanceDto == null)
            {
                return BadRequest("Performance data is null.");
            }
            var performance = new Performance
            {
                PlayId = performanceDto.PlayId,
                Audience = performanceDto.Audience
            };
            _db.Performances.Add(performance);
            _db.SaveChanges();

            return CreatedAtAction(nameof(GetPerformance), new { id = performance.Id }, performance);
        }

        [HttpGet("{id}")]
        public IActionResult GetPerformance(int id)
        {
            var performance = _db.Performances.Find(id);
            if (performance == null)
            {
                return NotFound();
            }

            return Ok(performance);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPerformances()
        {
            var performances = await _db.Performances.ToListAsync();
            return Ok(performances);
        }
    }
}
