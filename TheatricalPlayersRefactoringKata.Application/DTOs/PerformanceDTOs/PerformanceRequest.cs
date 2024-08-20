namespace TheatricalPlayersRefactoringKata.Application.DTOs.PerformanceDTOs
{
    public record PerformanceRequest(
        Guid PlayId,
        int Audience);

}
