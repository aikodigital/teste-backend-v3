using Microsoft.EntityFrameworkCore;
using TheatricalPlayersRefactoringKata.Domain.Models;
using TheatricalPlayersRefactoringKata.Infrastructure.Data;
using TheatricalPlayersRefactoringKata.Infrastructure.Exceptions;
using TheatricalPlayersRefactoringKata.Infrastructure.Repositories.Interfaces;

namespace TheatricalPlayersRefactoringKata.Infrastructure.Repositories;

/// <summary>
/// Repository for managing Invoice entities in the database.
/// </summary>
public class InvoiceRepository : IInvoiceRepository
{
    private readonly AppDbContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="InvoiceRepository"/> class.
    /// </summary>
    /// <param name="context">The database context to be used by the repository.</param>
    public InvoiceRepository(AppDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Gets an invoice by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the invoice.</param>
    /// <returns>The invoice with the specified identifier.</returns>
    /// <exception cref="EntityNotFoundException">Thrown when the invoice with the specified identifier is not found.</exception>
    public async Task<Invoice> GetByIdAsync(Guid id)
    {
        return await _context.Invoices
            .Include(x => x.Customer)
            .Include(x => x.Performances)
            .ThenInclude(x => x.Play)
            .FirstOrDefaultAsync(x => x.Id == id) ?? 
               throw new EntityNotFoundException($"Entity with ID {id} was not found.");
    }

    /// <summary>
    /// Gets all invoices.
    /// </summary>
    /// <returns>A list of all invoices.</returns>
    public async Task<IEnumerable<Invoice>> GetAllAsync()
    {
        return await _context.Invoices
            .Include(x => x.Customer)
            .Include(x => x.Performances)
            .ThenInclude(x => x.Play)
            .AsNoTracking()
            .ToListAsync();
    }

    /// <summary>
    /// Adds a new invoice to the database.
    /// </summary>
    /// <param name="invoice">The invoice to be added.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task AddAsync(Invoice invoice)
    {
        await _context.Invoices.AddAsync(invoice);
    }
}