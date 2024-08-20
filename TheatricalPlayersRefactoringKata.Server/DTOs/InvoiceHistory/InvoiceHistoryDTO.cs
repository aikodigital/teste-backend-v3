using TheatricalPlayersRefactoringKata.Server.Database.DTOs.PerformanceHistory;

namespace TheatricalPlayersRefactoringKata.Server.Database.DTOs.InvoiceHistory
{
    public class InvoiceHistoryDTO
    {
        public required string Customer { get; set; }
        public required decimal TotalAmountOwed { get; set; }
        public required decimal TotalEarnedCredits { get; set; }
        public required string DateOfInvoice { get; set; }
        public required IEnumerable<PerformanceHistoryDTO> PerformancesHistories { get; set; }
    }
}