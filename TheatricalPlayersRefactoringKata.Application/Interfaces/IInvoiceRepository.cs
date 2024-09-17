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
        List<Performance> GetPerformancesByInvoiceId(string invoiceId);
        List<Play> GetAllPlays();
        List<InvoiceCalculeteSettings> GetInvoiceCalculeteSettings();
        List<InvoiceCreditSettings> GetInvoiceCreditSettings();
    }
}
