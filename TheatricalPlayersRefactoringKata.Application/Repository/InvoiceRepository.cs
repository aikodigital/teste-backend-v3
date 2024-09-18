using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Application.Interfaces;
using TheatricalPlayersRefactoringKata.Domain;
using TheatricalPlayersRefactoringKata.Domain.Entities;
using TheatricalPlayersRefactoringKata.Infrastructure.Data;

namespace TheatricalPlayersRefactoringKata.Application.Repository
{
    /// <summary>
    /// Repository class for accessing invoice-related data from the database.
    /// Implements <see cref="IInvoiceRepository"/>.
    /// </summary>
    public class InvoiceRepository : IInvoiceRepository
    {
        /// <summary>
        /// Retrieves a list of all plays from the database.
        /// </summary>
        /// <returns>A list of <see cref="Play"/> objects.</returns>
        public List<Play> GetAllPlays()
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                return context.Play.ToList();
            }
        }

        /// <summary>
        /// Asynchronously retrieves a list of all plays from the database.
        /// </summary>
        /// <returns>A task representing the asynchronous operation. The task result contains a list of <see cref="Play"/> objects.</returns>
        public async Task<List<Play>> GetAllPlaysAsync()
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                return await context.Play.ToListAsync();
            }
        }

        /// <summary>
        /// Retrieves an invoice by its ID from the database.
        /// </summary>
        /// <param name="invoiceId">The ID of the invoice to retrieve.</param>
        /// <returns>The <see cref="Invoice"/> object if found; otherwise, null.</returns>
        public Invoice GetInvoiceById(string invoiceId)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                return context.Invoices
                    .Where(x => x.InvoiceId.ToString().Equals(invoiceId))
                    .FirstOrDefault();
            }
        }

        /// <summary>
        /// Asynchronously retrieves an invoice by its ID from the database.
        /// </summary>
        /// <param name="invoiceId">The ID of the invoice to retrieve.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains the <see cref="Invoice"/> object if found; otherwise, null.</returns>
        public async Task<Invoice> GetInvoiceByIdAsync(string invoiceId)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                return await context.Invoices
                    .Where(x => x.InvoiceId.ToString().Equals(invoiceId))
                    .FirstOrDefaultAsync();
            }
        }

        /// <summary>
        /// Retrieves a list of invoice calculation settings from the database.
        /// </summary>
        /// <returns>A list of <see cref="InvoiceCalculeteSettings"/> objects.</returns>
        public List<InvoiceCalculeteSettings> GetInvoiceCalculeteSettings()
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                return context.InvoiceCalculeteSettings.ToList();
            }
        }

        /// <summary>
        /// Asynchronously retrieves a list of invoice calculation settings from the database.
        /// </summary>
        /// <returns>A task representing the asynchronous operation. The task result contains a list of <see cref="InvoiceCalculeteSettings"/> objects.</returns>
        public async Task<List<InvoiceCalculeteSettings>> GetInvoiceCalculeteSettingsAsync()
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                return await context.InvoiceCalculeteSettings.ToListAsync();
            }
        }

        /// <summary>
        /// Retrieves a list of invoice credit settings from the database.
        /// </summary>
        /// <returns>A list of <see cref="InvoiceCreditSettings"/> objects.</returns>
        public List<InvoiceCreditSettings> GetInvoiceCreditSettings()
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                return context.InvoiceCreditSettings.ToList();
            }
        }

        /// <summary>
        /// Asynchronously retrieves a list of invoice credit settings from the database.
        /// </summary>
        /// <returns>A task representing the asynchronous operation. The task result contains a list of <see cref="InvoiceCreditSettings"/> objects.</returns>
        public async Task<List<InvoiceCreditSettings>> GetInvoiceCreditSettingsAsync()
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                return await context.InvoiceCreditSettings.ToListAsync();
            }
        }

        /// <summary>
        /// Retrieves a list of performances associated with a specific invoice ID from the database.
        /// </summary>
        /// <param name="invoiceId">The ID of the invoice whose performances are to be retrieved.</param>
        /// <returns>A list of <see cref="Performance"/> objects associated with the specified invoice ID.</returns>
        public List<Performance> GetPerformancesByInvoiceId(string invoiceId)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                return context.Performances
                    .Where(x => x.InvoiceId.ToString().Equals(invoiceId))
                    .ToList();
            }
        }

        /// <summary>
        /// Asynchronously retrieves a list of performances associated with a specific invoice ID from the database.
        /// </summary>
        /// <param name="invoiceId">The ID of the invoice whose performances are to be retrieved.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains a list of <see cref="Performance"/> objects associated with the specified invoice ID.</returns>
        public async Task<List<Performance>> GetPerformancesByInvoiceIdAsync(string invoiceId)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                return await context.Performances
                    .Where(x => x.InvoiceId.ToString().Equals(invoiceId))
                    .ToListAsync();
            }
        }
    }
}
