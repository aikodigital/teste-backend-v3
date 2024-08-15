using TheatricalPlayersRefactoringKata.Domain.Entities;

namespace TheatricalPlayersRefactoringKata.Domain.Repos;
public interface IInvoice
{
    Task Add(Invoice invoice);
    Task<List<Invoice>> GetAll();
    Task<Invoice?> GetByCustomer(string name);
}
