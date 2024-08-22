using TheatricalPlayersRefactoringKata.Domain.Enums;

namespace TheatricalPlayersRefactoringKata.Application.DTOs.PlayDTOs
{
    public record PlayResponse(
        Guid Id,
        string Name,
        int Lines,
        Genre Genre);
}
