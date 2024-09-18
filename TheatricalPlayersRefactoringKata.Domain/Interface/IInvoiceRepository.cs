using TheatricalPlayersRefactoringKata.Domain.Entities;

namespace TheatricalPlayersRefactoringKata.Domain.Interface
{
    public interface IInvoiceRepository
    {
        public Task<bool> Create(Invoice invoice);
        public Task<bool> Delete(string id);
        public Task<IEnumerable<Invoice>> GetAllByCustomer(string customerName);
        public Task<IEnumerable<Invoice>> GetAllByPlay(string playId);
    }
}
