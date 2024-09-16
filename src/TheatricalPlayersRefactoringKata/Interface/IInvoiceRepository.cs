using System.Collections.Generic;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Entities;

namespace TheatricalPlayersRefactoringKata.Interface
{
    public interface IInvoiceRepository
    {
        Task<int> Create(Invoice invoice);
        Task<Invoice?> Get(int id);
        List<Invoice> GetAll(int take, int page);
        void Delete(Invoice invoice);
    }
}
