#region

using Microsoft.AspNetCore.Mvc;
using TheatricalPlayersRefactoringKata.API.Repositories;
using TheatricalPlayersRefactoringKata.API.Repositories.DTOs;
using TheatricalPlayersRefactoringKata.API.Repositories.Validators;
using TheatricalPlayersRefactoringKata.Core.Entities;

#endregion

namespace TheatricalPlayersRefactoringKata.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PlayController(PlayRepository repo) : ControllerBase
{
    // GET: api/<PlayController>
    [HttpGet]
    public IEnumerable<PlayResponse> Get()
    {
        return repo.GetPlays().Result;
    }

    // GET api/<PlayController>/5
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get(Guid id)
    {
        return id == Guid.Empty ? 
            new BadRequestObjectResult("Invalid play id") : 
            await repo.GetPlayById(id).ConfigureAwait(false);
    }

    // POST api/<PlayController>
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] PlayRequest play)
    {
        return PlayValidator.IsValid(play) ? 
            new BadRequestObjectResult("Invalid play request") : 
            await repo.CreatePlay(play).ConfigureAwait(false);
    }
    
    // DELETE api/<PlayController>/5
    [HttpDelete("{id:guid}")]
    public Task<IActionResult> Delete(Guid id)
    {
        return id == Guid.Empty ? 
            Task.FromResult<IActionResult>(new BadRequestObjectResult("Invalid play id")) : 
            repo.DeletePlay(id);
    }
}