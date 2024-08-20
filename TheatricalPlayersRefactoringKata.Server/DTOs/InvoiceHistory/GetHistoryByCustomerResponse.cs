using TheatricalPlayersRefactoringKata.Server.Database.DTOs.InvoiceHistory;

namespace TheatricalPlayersRefactoringKata.Server.DTOs.InvoiceHistory
{
    public class GetHistoryByCustomerResponse
    {
        public required List<InvoiceHistoryDTO> InvoicesHistory { get; set; }
    }
}