#region

using Microsoft.AspNetCore.Mvc;
using TheatricalPlayersRefactoringKata.API.Repositories.DTOs;
using TheatricalPlayersRefactoringKata.Core.Entities;

#endregion

namespace TheatricalPlayersRefactoringKata.API.Repositories.Interfaces;

public interface IPlayRepository
{
    Task<IActionResult> CreatePlay(PlayRequest play);
    Task<IEnumerable<PlayResponse>> GetPlays();
    Task<IActionResult> GetPlayById(Guid playId);
    Task<IActionResult> UpdatePlay(Guid playId, PlayRequest play);
    Task<IActionResult> DeletePlay(Guid playId);
}