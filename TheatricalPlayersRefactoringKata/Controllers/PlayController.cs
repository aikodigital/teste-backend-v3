using Microsoft.AspNetCore.Mvc;
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

        /// <summary>
        /// Endpoint for creating a new Play. 
        /// Play Types: 0 - Tragedy | 1 - Comedy | 2 - History 
        /// </summary>
        /// <param name="name">Play Name</param>
        /// <param name="lines">Quantity of Lines</param>
        /// <param name="type">Play Type</param>
        [HttpPost("Create")]
        public Task<ActionResult> Create(string name, int lines, int type)
        {
            try
            {
                return _playsService.Create(new PlayModel(name, lines, (TypePlay)type));
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Endpoint to return Play from a specific Play Name
        /// </summary>
        /// <param name="name">Play Name</param>
        [HttpGet("GetByName")]
        public Task<ActionResult> GetByName(string name)
        {
            try
            {
                return _playsService.GetByName(name);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Endpoint to return all registered Plays
        /// </summary>
        [HttpGet("GetAll")]
        public Task<ActionResult> GetAll()
        {
            try
            {
                return _playsService.GetAll();
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Endpoint to update a specific Play by PlayModel
        /// </summary>
        /// <param name="play">Play Model</param>
        [HttpPut("Update")]
        public Task<ActionResult> Update(PlayModel play)
        {
            try
            {
                return _playsService.Update(play);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Endpoint to delete a specific Play by Play Id
        /// </summary>
        /// <param name="id">Play ID</param>
        [HttpDelete("Delete")]
        public Task<ActionResult> Delete(string id)
        {
            try
            {
                return _playsService.Delete(id);
            }
            catch
            {
                throw;
            }
        }
    }
}
