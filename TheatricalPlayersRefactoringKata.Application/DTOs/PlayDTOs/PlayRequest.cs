using TheatricalPlayersRefactoringKata.Domain.Enums;

namespace TheatricalPlayersRefactoringKata.Application.DTOs.PlayDTOs
{
    public record PlayRequest(
        string Name,
        int Lines,
        Genre Genre);
}
