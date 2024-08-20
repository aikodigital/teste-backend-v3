namespace TheatricalPlayersRefactoringKata.Application.DTOs.PerformanceDTOs
{
    public record PerformanceResponse(
        Guid Id,
        Guid PlayId,
        int Audience,
        int Credits);
}
