using TheatricalPlayersRefactoringKata.Domain.Model.Entity;

namespace TheatricalPlayersRefactoringKata.Domain.Interface.Services
{
    public interface IInvoiceService
    {
        Task<Invoice> Invoice(long customerId, List<Performance> performances, List<Play> plays);
        string GenerateXML(Invoice invoice);
        string GenerateText(Invoice invoice);
        string GenerateJson(Invoice invoice);
    }
}