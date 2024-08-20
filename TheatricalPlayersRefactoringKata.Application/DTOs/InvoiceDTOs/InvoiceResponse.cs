using TheatricalPlayersRefactoringKata.Application.DTOs.PerformanceDTOs;

namespace TheatricalPlayersRefactoringKata.Application.DTOs.InvoiceDTOs
{
    public record InvoiceResponse(
        Guid Id,
        string Customer,
        List<PerformanceResponse> Performances);
}
