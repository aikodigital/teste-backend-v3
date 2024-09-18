using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Domain.Entities;
using TheatricalPlayersRefactoringKata.Domain;

namespace TheatricalPlayersRefactoringKata.Application.Interfaces
{
    /// <summary>
    /// Defines the contract for repository operations related to invoices, performances, plays, and settings.
    /// </summary>
    public interface IInvoiceRepository
    {
        /// <summary>
        /// Retrieves an invoice by its unique identifier.
        /// </summary>
        /// <param name="invoiceId">The unique identifier of the invoice.</param>
        /// <returns>The <see cref="Invoice"/> with the specified identifier.</returns>
        Invoice GetInvoiceById(string invoiceId);

        /// <summary>
        /// Asynchronously retrieves an invoice by its unique identifier.
        /// </summary>
        /// <param name="invoiceId">The unique identifier of the invoice.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the <see cref="Invoice"/> with the specified identifier.</returns>
        Task<Invoice> GetInvoiceByIdAsync(string invoiceId);

        /// <summary>
        /// Retrieves a list of performances associated with a specified invoice identifier.
        /// </summary>
        /// <param name="invoiceId">The unique identifier of the invoice.</param>
        /// <returns>A list of <see cref="Performance"/> objects associated with the specified invoice identifier.</returns>
        List<Performance> GetPerformancesByInvoiceId(string invoiceId);

        /// <summary>
        /// Asynchronously retrieves a list of performances associated with a specified invoice identifier.
        /// </summary>
        /// <param name="invoiceId">The unique identifier of the invoice.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a list of <see cref="Performance"/> objects associated with the specified invoice identifier.</returns>
        Task<List<Performance>> GetPerformancesByInvoiceIdAsync(string invoiceId);

        /// <summary>
        /// Retrieves a list of all plays.
        /// </summary>
        /// <returns>A list of <see cref="Play"/> objects representing all available plays.</returns>
        List<Play> GetAllPlays();

        /// <summary>
        /// Asynchronously retrieves a list of all plays.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains a list of <see cref="Play"/> objects representing all available plays.</returns>
        Task<List<Play>> GetAllPlaysAsync();

        /// <summary>
        /// Retrieves a list of invoice calculation settings.
        /// </summary>
        /// <returns>A list of <see cref="InvoiceCalculeteSettings"/> objects representing the calculation settings for invoices.</returns>
        List<InvoiceCalculeteSettings> GetInvoiceCalculeteSettings();

        /// <summary>
        /// Asynchronously retrieves a list of invoice calculation settings.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains a list of <see cref="InvoiceCalculeteSettings"/> objects representing the calculation settings for invoices.</returns>
        Task<List<InvoiceCalculeteSettings>> GetInvoiceCalculeteSettingsAsync();

        /// <summary>
        /// Retrieves a list of invoice credit settings.
        /// </summary>
        /// <returns>A list of <see cref="InvoiceCreditSettings"/> objects representing the credit settings for invoices.</returns>
        List<InvoiceCreditSettings> GetInvoiceCreditSettings();

        /// <summary>
        /// Asynchronously retrieves a list of invoice credit settings.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains a list of <see cref="InvoiceCreditSettings"/> objects representing the credit settings for invoices.</returns>
        Task<List<InvoiceCreditSettings>> GetInvoiceCreditSettingsAsync();
    }
}
