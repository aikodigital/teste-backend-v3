using System;
using TheatricalPlayersRefactoringKata.Application.Interfaces;
using TheatricalPlayersRefactoringKata.Domain;
using TheatricalPlayersRefactoringKata.Domain.Entities;

namespace TheatricalPlayersRefactoringKata.Application.Services
{
    /// <summary>
    /// Service class for calculating credit based on audience size and credit settings.
    /// Implements <see cref="ICalculateCreditAudience"/>.
    /// </summary>
    public class CalculateCreditAudience : ICalculateCreditAudience
    {
        /// <summary>
        /// Calculates the credit amount based on audience size and invoice credit settings.
        /// </summary>
        /// <param name="audience">The number of audience members for the event.</param>
        /// <param name="invoiceCreditSettings">The settings that determine how credit is calculated.</param>
        /// <returns>The calculated credit amount as a <see cref="decimal"/>.</returns>
        /// <remarks>
        /// The credit amount is calculated as follows:
        /// - If the audience exceeds the minimum required for credits, the excess number of attendees is used to calculate volume credits.
        /// - Additional credits are added based on the number of attendees relative to the bonus credit per attendee setting.
        /// </remarks>
        public decimal CalculateCredit(int audience, InvoiceCreditSettings invoiceCreditSettings)
        {
            var volumeCredits = Math.Max(audience - invoiceCreditSettings.MinimumAudience, 0);
            if (invoiceCreditSettings.BonusCreditPerAttendees > 0)
                volumeCredits += (int)Math.Floor(audience / invoiceCreditSettings.BonusCreditPerAttendees);

            return volumeCredits;
        }
    }
}
