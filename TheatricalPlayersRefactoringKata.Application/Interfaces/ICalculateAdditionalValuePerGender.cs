namespace TheatricalPlayersRefactoringKata.Application.Interfaces
{
    /// <summary>
    /// Defines a contract for calculating additional values based on play type.
    /// </summary>
    public interface ICalculateAdditionalValuePerPlayType
    {
        /// <summary>
        /// Calculates the additional value for a play based on audience size and play type settings.
        /// </summary>
        /// <param name="audience">The number of attendees for the play.</param>
        /// <param name="audienceMinimum">The minimum audience size required to receive additional value.</param>
        /// <param name="bonus">The bonus amount given for exceeding the minimum audience size.</param>
        /// <param name="per_audience_additional">The additional amount given for each attendee above the minimum audience size.</param>
        /// <param name="per_audience">The amount given for each attendee, regardless of the minimum audience size.</param>
        /// <returns>The calculated additional value based on the specified parameters.</returns>
        decimal CalculateAdditionalValue(int audience, int audienceMinimum, decimal bonus, decimal per_audience_additional, decimal per_audience);
    }
}
