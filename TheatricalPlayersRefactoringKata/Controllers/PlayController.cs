using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Application.Interfaces;
using TheatricalPlayersRefactoringKata.Application.Models;

namespace TheatricalPlayersRefactoringKata.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayController : ControllerBase
    {
        private readonly IPlayService _playsService;

        public PlayController(IPlayService playsService)
        {
            _playsService = playsService;
        }

        [HttpPost("Create")]
        public Task<ActionResult> Create(PlayModel play)
        {
            return _playsService.Create(play);
        }

        [HttpGet("GetByName")]
        public Task<ActionResult> GetByName(string name)
        {
            return _playsService.GetByName(name);
        }

        [HttpGet("GetAll")]
        public Task<ActionResult> GetAll()
        {
            return _playsService.GetAll();
        }

        [HttpPut("Update")]
        public Task<ActionResult> Update(PlayModel play, string id)
        {
            return _playsService.Update(play, id);
        }

        [HttpDelete("Delete")]
        public Task<ActionResult> Delete(string id)
        {
            return _playsService.Delete(id);
        }
    }
}
