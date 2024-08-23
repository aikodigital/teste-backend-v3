using Microsoft.AspNetCore.Mvc;
using TheatricalPlayersRefactoringKata.API.Data;
using TheatricalPlayersRefactoringKata.API.Repositories.DTOs;
using TheatricalPlayersRefactoringKata.API.Repositories.Interfaces;
using TheatricalPlayersRefactoringKata.Core.Entities;

namespace TheatricalPlayersRefactoringKata.API.Repositories;

public class PerfRepository(ApiDbContext context): IPerformanceRepository
{
    public async Task<IActionResult> CreatePerformance(PerfRequest perf, Guid playId)
    {
        var play = await context.Plays.FindAsync(playId);
        if (play == null)
        {
            return new NotFoundResult();
        }
        
        var newPerf= new Performance( play ,perf.Audience,[], playId);
        await context.Performances.AddAsync(newPerf);
        await context.SaveChangesAsync();
        
        return new CreatedResult("performance", newPerf);
    }

    public async Task<IEnumerable<PerfResponse>> GetPerformances()
    {
        var performances = context.Performances.Select(p =>
            new PerfResponse(p.Id, p.Audience, p.Amount, p.CalculateCredits(), p.Play!.Name));
        
        
        return await Task.FromResult<IEnumerable<PerfResponse>>(performances);
    }

    public async Task<IActionResult> GetPerformancesById(Guid performanceId)
    {
        var performance = await context.Performances.FindAsync(performanceId);

        if (performance == null)
        {
            return new NotFoundResult();
        }
        
        var play = await context.Plays.FindAsync(performance.PlayId);
        
        if (play == null)
        {
            return new NotFoundResult();
        }
            
        var response = new PerfResponse(performance.Id, performance.Audience, performance.Amount, performance.CalculateCredits(), play.Name);

        return new OkObjectResult(response);
    }
    
    public async Task<IActionResult> DeletePerformance(Guid performanceId)
    {
        var performance = await context.Performances.FindAsync(performanceId);
        if (performance == null)
        {
            return new NotFoundResult();
        }

        context.Performances.Remove(performance);
        await context.SaveChangesAsync();

        return new NoContentResult();
    }
}