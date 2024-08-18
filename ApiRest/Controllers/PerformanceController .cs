using Microsoft.AspNetCore.Mvc;
using TheatricalPlayersRefactoringKata.Models;
using TheatricalPlayersRefactoringKata.DTOs;
using TheatricalPlayersRefactoringKata.API.Repositories.Interfaces;

namespace TheatricalPlayersRefactoringKata.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PerformanceController : ControllerBase
    {
        private readonly IPerformanceRepository _performanceRepository;

        public PerformanceController(IPerformanceRepository performanceRepository)
        {
            _performanceRepository = performanceRepository;
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreatePerformance([FromBody] PerformanceDto performanceDto)
        {
            var performance = new Performance
            {
                PlayId = performanceDto.PlayId,
                Audience = performanceDto.Audience
            };

            await _performanceRepository.AddAsync(performance);

            return CreatedAtAction(nameof(GetPerformance), new { id = performance.Id }, performance);
        }
        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPerformance(int id)
        {
            var performance = await _performanceRepository.GetByIdAsync(id);
            if (performance == null)
            {
                return NotFound();
            }

            return Ok(performance);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPerformances()
        {
            var performances = await _performanceRepository.GetAllAsync();
            return Ok(performances);
        }
    }
}
