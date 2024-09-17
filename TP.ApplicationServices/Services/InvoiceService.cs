using TP.Domain.Entities;
using TP.Infrastructure.Repositories;

namespace TP.Service
{
    public class InvoiceService
    {
        private readonly IInvoiceRepository _invoiceRepository;

        public InvoiceService(IInvoiceRepository invoiceRepository)
        {
            _invoiceRepository = invoiceRepository;
        }

        public async Task<IEnumerable<Invoice>> GetAllInvoicesAsync()
        {
            return await _invoiceRepository.GetAllAsync();
        }

        public async Task<Invoice> GetInvoiceByIdAsync(Guid id)
        {
            return await _invoiceRepository.GetByIdAsync(id);
        }

        public async Task AddInvoiceAsync(Invoice invoice)
        {
            await _invoiceRepository.AddAsync(invoice);
        }

        public void UpdateInvoice(Invoice invoice)
        {
            _invoiceRepository.Update(invoice);
        }

        public void DeleteInvoice(Guid id)
        {
            var invoice = _invoiceRepository.GetByIdAsync(id).Result;
            if (invoice != null)
            {
                _invoiceRepository.Delete(invoice);
            }
        }
    }
}
