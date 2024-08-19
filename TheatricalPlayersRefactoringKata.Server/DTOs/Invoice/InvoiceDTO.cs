using TheatricalPlayersRefactoringKata.Server.Database.DTOs.Performance;

namespace TheatricalPlayersRefactoringKata.Server.Database.DTOs.Invoice
{
    public class InvoiceDTO
    {
        public required string Customer { get; set; }
        public required List<PerformanceDTO> Performances { get; set; }
        public required string OutputWritterType { get; set; }
    }
}