using TheatricalPlayersRefactoringKata.Server.Database.DTOs.Invoice;

namespace TheatricalPlayersRefactoringKata.Server.DTOs.Play
{
    public class SimulateInvoiceRequest
    {
        public required InvoiceDTO Invoice { get; set; }
    }
}