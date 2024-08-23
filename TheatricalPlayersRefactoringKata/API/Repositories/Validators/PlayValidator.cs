using TheatricalPlayersRefactoringKata.API.Repositories.DTOs;
using TheatricalPlayersRefactoringKata.Core.Services;

namespace TheatricalPlayersRefactoringKata.API.Repositories.Validators;

public static class PlayValidator 
{
    public static bool IsValid(PlayRequest? playRequest)
    { 
        return !string.IsNullOrEmpty(playRequest?.Name) && playRequest.Lines > 0 && !Enum.IsDefined(typeof(Genre), playRequest.Type);
    }
}