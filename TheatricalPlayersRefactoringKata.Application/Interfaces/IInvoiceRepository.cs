using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Domain.Entities;
using TheatricalPlayersRefactoringKata.Domain;

namespace TheatricalPlayersRefactoringKata.Application.Interfaces
{
    public interface IInvoiceRepository
    {
        Invoice GetInvoiceById(string invoiceId);
        Task<Invoice> GetInvoiceByIdAsync(string invoiceId);
        List<Performance> GetPerformancesByInvoiceId(string invoiceId);
        Task<List<Performance>> GetPerformancesByInvoiceIdAsync(string invoiceId);
        List<Play> GetAllPlays();
        Task<List<Play>> GetAllPlaysAsync();
        List<InvoiceCalculeteSettings> GetInvoiceCalculeteSettings();
        Task<List<InvoiceCalculeteSettings>> GetInvoiceCalculeteSettingsAsync();
        List<InvoiceCreditSettings> GetInvoiceCreditSettings();
        Task<List<InvoiceCreditSettings>> GetInvoiceCreditSettingsAsync();
    }
}
