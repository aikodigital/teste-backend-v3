namespace TheatricalPlayersRefactoringKata.Application.DTOs.InvoiceDTOs
{
    public record InvoiceRequest(
        string Customer,
        List<Guid> PerformanceIds);
}