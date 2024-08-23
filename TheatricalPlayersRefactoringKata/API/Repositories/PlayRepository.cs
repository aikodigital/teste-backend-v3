#region

using Microsoft.AspNetCore.Mvc;
using TheatricalPlayersRefactoringKata.API.Data;
using TheatricalPlayersRefactoringKata.API.Repositories.DTOs;
using TheatricalPlayersRefactoringKata.API.Repositories.Interfaces;
using TheatricalPlayersRefactoringKata.API.Repositories.Validators;
using TheatricalPlayersRefactoringKata.Core.Entities;
using TheatricalPlayersRefactoringKata.Core.Services;

#endregion

namespace TheatricalPlayersRefactoringKata.API.Repositories;

public class PlayRepository(ApiDbContext context) : IPlayRepository
{
    public async Task<IActionResult> CreatePlay(PlayRequest play)
    {
        var newPlay = new Play(play.Name, play.Lines, play.Type);

        context.Plays.Add(newPlay);
       await context.SaveChangesAsync();

        return new CreatedResult("play", newPlay);
    }

    public Task<IEnumerable<PlayResponse>> GetPlays()
    {
        var plays = context.Plays.Select(p => new PlayResponse(p.Id, p.Name, p.Type, p.Lines));
        return Task.FromResult<IEnumerable<PlayResponse>>(plays);
    }

    public async Task<IActionResult> GetPlayById(Guid playId)
    {
        var play = await context.Plays.FindAsync(playId);
        
        if (play == null)
        {
            return new NotFoundResult();
        }
        
        return new OkObjectResult(new PlayResponse(play.Id, play.Name, play.Type, play.Lines));
    }

    public async Task<IActionResult> UpdatePlay(Guid playId, PlayRequest play)
    {
        var existingPlay = await context.Plays.FindAsync(playId);
        
        if (existingPlay == null)
        {
            return new NotFoundResult();
        }

        existingPlay.Name = play.Name;
        existingPlay.Type = play.Type;
        existingPlay.Lines = play.Lines;

        await context.SaveChangesAsync();

        return new OkObjectResult(new PlayResponse(existingPlay.Id, existingPlay.Name, existingPlay.Type, existingPlay.Lines));
    }

    public async Task<IActionResult> DeletePlay(Guid playId)
    {
        var play = await context.Plays.FindAsync(playId);
        
        if (play == null)
        {
            return new NotFoundResult();
        }

        context.Plays.Remove(play);
        await context.SaveChangesAsync();

        return new OkResult();
    }
}