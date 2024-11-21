using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheatricalPlayersRefactoringKata.Domain.Interfaces
{
    public interface IGenreStrategy
    {
        /// <summary>
        /// Calculates the total cost for the performance based on the genre.
        /// </summary>
        /// <param name="audience">Audience size.</param>
        /// <param name="basePrice">Base price calculated for the play.</param>
        /// <returns>The total cost to be charged.</returns>
        decimal CalculateCost(int audience, int lines);

        /// <summary>
        /// Calculates the credits earned by the customer based on the genre.
        /// </summary>
        /// <param name="audience">Audience size.</param>
        /// <returns>Total credits earned.</returns>
        int CalculateCredits(int audience);

        /// <summary>
        /// Calculates the base price for the play based on the number of lines.
        /// </summary>
        /// <param name="lines">Number of lines in the play.</param>
        /// <returns>The calculated base price.</returns>
        decimal CalculateBasePrice(int lines);
    }
}
