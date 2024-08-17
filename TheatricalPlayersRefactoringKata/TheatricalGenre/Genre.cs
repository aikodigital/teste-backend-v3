using System;

namespace TheatricalPlayersRefactoringKata.TheatricalGenre
{
    public abstract class Genre
    {
        /// <summary>
        /// Calculates the amount for a performance based on the number of lines.
        /// </summary>
        /// <param name="audience">The number of audience members.</param>
        /// <param name="lines">The number of lines in the performance.</param>
        /// <returns>The amount in cents.</returns>
        public virtual int CalculateAmount(int audience, int lines)
        {
            if (lines < 1000) lines = 1000;
            if (lines > 4000) lines = 4000;
            return lines * 10;
        }

        /// <summary>
        /// Calculates volume credits based on the audience size.
        /// </summary>
        /// <param name="audience">The number of audience members attending the performance.</param>
        /// <returns>The volume credits earned.</returns>
        public virtual int CalculateVolumeCredits(int audience)
        {
            return Math.Max(audience - 30, 0);
        }
    }
}