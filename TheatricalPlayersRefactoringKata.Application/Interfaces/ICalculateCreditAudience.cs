using TheatricalPlayersRefactoringKata.Domain.Entities;

namespace TheatricalPlayersRefactoringKata.Application.Interfaces
{
    /// <summary>
    /// Defines a contract for calculating credit based on the audience and credit settings.
    /// </summary>
    public interface ICalculateCreditAudience
    {
        /// <summary>
        /// Calculates the credit earned based on the audience size and the invoice credit settings.
        /// </summary>
        /// <param name="audience">The number of attendees for the performance.</param>
        /// <param name="invoiceCreditSettings">The settings that define how credits are calculated for the performance.</param>
        /// <returns>The calculated credit based on the audience size and settings.</returns>
        decimal CalculateCredit(int audience, InvoiceCreditSettings invoiceCreditSettings);
    }
}
