using TheatricalPlayersRefactoringKata.Server.Database.DTOs.InvoiceHistory;

namespace TheatricalPlayersRefactoringKata.Server.DTOs.InvoiceHistory
{
    public class GetHistoryByIdResponse
    {
        public required InvoiceHistoryDTO InvoiceHistory { get; set; }
    }
}