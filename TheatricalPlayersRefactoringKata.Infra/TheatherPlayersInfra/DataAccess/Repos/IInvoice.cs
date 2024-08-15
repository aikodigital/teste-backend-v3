
namespace TheatherPlayersInfra.DataAccess.Repos;
public interface IInvoice
{
    Task Add(Invoice invoice);
    Task Delete(Invoice invoice);
    Task Update(Invoice invoice);
    Task<List<Invoice>> GetAll();
    Task<Invoice?> GetById(long id);
}
