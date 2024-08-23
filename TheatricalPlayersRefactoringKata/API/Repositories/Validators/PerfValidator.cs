using TheatricalPlayersRefactoringKata.API.Repositories.DTOs;
using TheatricalPlayersRefactoringKata.Core.Entities;

namespace TheatricalPlayersRefactoringKata.API.Repositories.Validators;

public static class PerfValidator
{
    public static bool IsValid(PerfRequest performance)
    {
        return performance is not { Audience: > 0 };
    }
}