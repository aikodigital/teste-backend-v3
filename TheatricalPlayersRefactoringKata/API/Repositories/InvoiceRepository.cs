using System.Data.Entity;
using Microsoft.AspNetCore.Mvc;
using TheatricalPlayersRefactoringKata.API.Data;
using TheatricalPlayersRefactoringKata.API.Repositories.DTOs;
using TheatricalPlayersRefactoringKata.API.Repositories.Interfaces;
using TheatricalPlayersRefactoringKata.Core.Entities;
using TheatricalPlayersRefactoringKata.Core.Printers;

namespace TheatricalPlayersRefactoringKata.API.Repositories;

public class InvoiceRepository(ApiDbContext context): IInvoiceRepository
{
    public async Task<IActionResult> CreateInvoice(InvoiceRequest invoice)
    {
        var dbInvoice = new Invoice()
        {
            Id = Guid.NewGuid(),
            Customer = invoice.CustomerName,
        };
        
        if(invoice.PerformancesIds != null && invoice.PerformancesIds.Count != 0)
        {
            dbInvoice.Performances = context.Performances.Where(p => invoice.PerformancesIds.Contains(p.Id)).ToList();
        }

        dbInvoice.Performances = [];
        
        context.Invoices.Add(dbInvoice);
        await context.SaveChangesAsync().ConfigureAwait(false);
        
        return new CreatedResult("invoice", dbInvoice);
    }

    public async Task<IActionResult> GetInvoiceById(Guid invoiceId)
    {
        var invoice = await context.Invoices.FindAsync(invoiceId);
        if(invoice == null)
        {
            return new NotFoundResult();
        }
        
        return new OkObjectResult(invoice);
    }

    public async Task<IEnumerable<Invoice>> GetInvoices()
    {
        var invoices = await context.Invoices.ToListAsync();
        return invoices;
    }

    public async Task<IActionResult> DeleteInvoice(Guid invoiceId)
    {
        var invoice = await context.Invoices.FindAsync(invoiceId);
        if(invoice == null)
        {
            return new NotFoundResult();
        }
        
        context.Invoices.Remove(invoice);
        await context.SaveChangesAsync().ConfigureAwait(false);
        
        return new NoContentResult();
    }

    public async Task<IActionResult> GenerateStatement(Guid invoiceId, ReceiptType receiptType)
    {
        var invoice = await context.Invoices.FindAsync(invoiceId);
        if(invoice == null)
        {
            return new NotFoundResult();
        }
        
        return receiptType switch
        {
            ReceiptType.Text => new OkObjectResult(TextStatementPrinter.Print(invoice)),
            ReceiptType.Xml => new OkObjectResult(XmlStatementPrinter.XmlMount(invoice)),
            _ => new BadRequestResult()
        };
    }
}