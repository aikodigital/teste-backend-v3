using System;
using TheatricalPlayersRefactoringKata.Application.Interfaces;
using TheatricalPlayersRefactoringKata.Domain;

namespace TheatricalPlayersRefactoringKata.Application.Services
{
    /// <summary>
    /// Service class for calculating additional value based on play type and audience metrics.
    /// Implements <see cref="ICalculateAdditionalValuePerPlayType"/>.
    /// </summary>
    public class CalculateAdditionalValuePerPlayType : ICalculateAdditionalValuePerPlayType
    {
        /// <summary>
        /// Calculates the additional value based on audience size, minimum audience requirement, and various bonus factors.
        /// </summary>
        /// <param name="audience">The number of audience members for the play.</param>
        /// <param name="audienceMinimum">The minimum number of audience members required to receive additional value.</param>
        /// <param name="bonus">A fixed bonus amount added if the audience exceeds the minimum requirement.</param>
        /// <param name="per_audience_additional">Additional amount per audience member beyond the minimum requirement.</param>
        /// <param name="per_audience">Amount per audience member regardless of the minimum requirement.</param>
        /// <returns>The calculated additional value as a <see cref="decimal"/>.</returns>
        public decimal CalculateAdditionalValue(int audience, int audienceMinimum, decimal bonus, decimal per_audience_additional, decimal per_audience)
        {
            var thisAmount = 0m;

            if (audience > audienceMinimum)
            {
                thisAmount += bonus
                              + per_audience_additional
                              * (audience - audienceMinimum);
            }

            if (per_audience > 0) thisAmount += per_audience * audience;

            return thisAmount;
        }
    }
}
