using TheatricalPlayersRefactoringKata.Domain.Entities;

namespace TheatricalPlayersRefactoringKata.Application.Interfaces
{
    /// <summary>
    /// Defines a contract for printing an invoice in a specific format.
    /// </summary>
    public interface IInvoicePrint
    {
        /// <summary>
        /// Generates a formatted string representation of the given invoice print statement.
        /// </summary>
        /// <param name="invoicePrint">The invoice print statement containing the details to be printed.</param>
        /// <returns>A string representation of the invoice print statement in the desired format.</returns>
        string Print(InvoicePrint.Statement invoicePrint);
    }
}
