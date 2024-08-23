#region

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TheatricalPlayersRefactoringKata.API.Data;
using TheatricalPlayersRefactoringKata.API.Repositories.DTOs;
using TheatricalPlayersRefactoringKata.API.Repositories.Interfaces;
using TheatricalPlayersRefactoringKata.Core.Entities;

#endregion

namespace TheatricalPlayersRefactoringKata.API.Repositories;

public class PlayRepository(ApiDbContext context) : IPlayRepository
{
    public async Task<IActionResult> CreatePlay(PlayRequest play)
    {
        var newPlay = new Play
        {
            Id = new Guid(),
            Name = play.Name,
            Type = play.Type,
            Lines = play.Lines
        };

        context.Plays.Add(newPlay);
       await context.SaveChangesAsync();

        return new CreatedResult("play", newPlay);
    }

    public async Task<IActionResult> GetPlays()
    {
        var plays = await context.Plays.Select(p => new PlayResponse(p.Id, p.Name, p.Type, p.Lines))
            .ToListAsync().ConfigureAwait(false);

        if (plays == null)
        {
            return new NotFoundResult();
        }
        
        return new OkObjectResult(plays);
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