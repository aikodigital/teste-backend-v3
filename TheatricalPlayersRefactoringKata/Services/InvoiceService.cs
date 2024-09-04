using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using TheatricalPlayersRefactoringKata.Entities;
using TheatricalPlayersRefactoringKata.Repositories.Interface;
using TheatricalPlayersRefactoringKata.Services.Interface;

namespace TheatricalPlayersRefactoringKata.Services
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IInvoiceRepository _invoiceRepository;

        public InvoiceService(IInvoiceRepository invoiceRepository)
        {
            _invoiceRepository = invoiceRepository;
        }

        public async Task<Invoice> CreateInvoiceAsync(Invoice invoice)
        {
            ValidateInvoice(invoice);

            var existingInvoice = await _invoiceRepository.GetInvoiceByIdAsync(invoice.Id);
            if (existingInvoice != null)
                throw new InvalidOperationException("An invoice with the same ID already exists.");

            await _invoiceRepository.AddInvoiceAsync(invoice);
            return invoice;
        }

        public async Task<Invoice> UpdateInvoiceAsync(Invoice invoice)
        {
            ValidateInvoice(invoice);

            var existingInvoice = await _invoiceRepository.GetInvoiceByIdAsync(invoice.Id);
            if (existingInvoice == null)
                throw new KeyNotFoundException("Invoice not found.");

            await _invoiceRepository.UpdateInvoiceAsync(invoice);
            return invoice;
        }

        public async Task<Invoice> GetInvoiceByIdAsync(int id)
        {
            var invoice = await _invoiceRepository.GetInvoiceByIdAsync(id);
            if (invoice == null)
                throw new KeyNotFoundException("Invoice not found.");

            return invoice;
        }

        public async Task DeleteInvoiceAsync(int id)
        {
            var invoice = await _invoiceRepository.GetInvoiceByIdAsync(id);
            if (invoice == null)
                throw new KeyNotFoundException("Invoice not found.");

            await _invoiceRepository.DeleteInvoiceAsync(id);
        }

        private void ValidateInvoice(Invoice invoice)
        {
            if (invoice == null)
                throw new ArgumentNullException(nameof(invoice), "Invoice cannot be null.");

            if (string.IsNullOrEmpty(invoice.Customer))
                throw new ArgumentException("Customer name cannot be null or empty.", nameof(invoice.Customer));

            if (invoice.Performances == null || invoice.Performances.Count == 0)
                throw new ArgumentException("Invoice must have at least one performance.", nameof(invoice.Performances));

            foreach (var performance in invoice.Performances)
            {
                if (string.IsNullOrEmpty(performance.PlayId))
                    throw new ArgumentException("Performance must have a PlayId.", nameof(performance.PlayId));

                if (performance.Audience < 0)
                    throw new ArgumentException("Audience cannot be negative.", nameof(performance.Audience));
            }
        }
    }
}
