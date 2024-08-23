#region

using Microsoft.AspNetCore.Mvc;
using TheatricalPlayersRefactoringKata.API.Repositories.DTOs;
using TheatricalPlayersRefactoringKata.Core.Entities;

#endregion

namespace TheatricalPlayersRefactoringKata.API.Repositories.Interfaces;

public interface IInvoiceRepository
{
    Task<IActionResult> CreateInvoice(InvoiceRequest invoice);
    Task<IActionResult> GetInvoiceById(Guid invoiceId);
    Task<IEnumerable<Invoice>> GetInvoices();
    Task<IActionResult> DeleteInvoice(Guid invoiceId);
    Task<IActionResult> GenerateStatement(Guid invoiceId, ReceiptType receiptType);
}

