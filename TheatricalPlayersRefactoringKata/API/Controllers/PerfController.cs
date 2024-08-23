using Microsoft.AspNetCore.Mvc;
using TheatricalPlayersRefactoringKata.API.Repositories.DTOs;
using TheatricalPlayersRefactoringKata.API.Repositories.Interfaces;
using TheatricalPlayersRefactoringKata.API.Repositories.Validators;

namespace TheatricalPlayersRefactoringKata.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PerfController(IPerformanceRepository repo) : ControllerBase
    {
        
        // GET: api/Perf
        [HttpGet]
        public async Task<IEnumerable<PerfResponse>> GetPerformances()
        {
            return await repo.GetPerformances().ConfigureAwait(false);
        }

        // GET: api/Perf/5
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetPerformance(Guid id)
        {
            return id == Guid.Empty ? BadRequest() : await repo.GetPerformancesById(id);
        }
        
        // POST: api/Perf
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async  Task<IActionResult> PostPerformance(PerfRequest perf, Guid playId)
        {
            return PerfValidator.IsValid(perf) && playId == Guid.Empty ? BadRequest() : await repo.CreatePerformance(perf, playId);
        }

        // DELETE: api/Perf/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePerformance(Guid id)
        {
            return id == Guid.Empty ? BadRequest() : await repo.DeletePerformance(id);
        }
    }
}
