using Microsoft.AspNetCore.Mvc;
using TheatricalPlayersRefactoringKata.API.DTO;
using TheatricalPlayersRefactoringKata.Entities;
using TheatricalPlayersRefactoringKata.Interfaces.Repositories;

namespace TheatricalPlayersRefactoringKata.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PerformanceController : Controller
    {
        private readonly IBaseRepository<Performance> _performanceRepository;

        public PerformanceController(IBaseRepository<Performance> context)
        {
            _performanceRepository = context;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] PerformanceDTO performanceDTO)
        {
            var performance = new Performance
            {
                PlayId = performanceDTO.PlayId,
                Audience = performanceDTO.Audience
            };

            await _performanceRepository.AddAsync(performance);
            return CreatedAtAction(nameof(Get), new { id = performance.Id }, performance);
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var performances = await _performanceRepository.GetAllAsync();
            return Ok(performances);
        }
    }
}
