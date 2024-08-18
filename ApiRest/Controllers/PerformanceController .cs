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
        private readonly ApiDbContext _db;

        public PerformanceController(ApiDbContext db)
        {
            _db = db;
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreatePerformance([FromBody] PerformanceDto performanceDto)
        {
            var performance = new Performance
            {
                PlayName = performanceDto.PlayName,
                Audience = performanceDto.Audience
            };

            _db.Performances.Add(performance);
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPerformance), new { id = performance.Id }, performance);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPerformance(int id)
        {
            var performance = await _db.Performances.FindAsync(id);
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
