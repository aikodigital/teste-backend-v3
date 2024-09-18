using System;
using TheatricalPlayersRefactoringKata.Application.Constants;
using TheatricalPlayersRefactoringKata.Application.Interfaces;
using TheatricalPlayersRefactoringKata.Domain;

namespace TheatricalPlayersRefactoringKata.Application.Services
{
    /// <summary>
    /// Service class for calculating the base amount based on the number of lines.
    /// Implements <see cref="ICalculateBaseAmountPerLine"/>.
    /// </summary>
    public class CalculateBaseAmountPerLine : ICalculateBaseAmountPerLine
    {
        /// <summary>
        /// Calculates the base amount based on the number of lines, applying minimum and maximum constraints.
        /// </summary>
        /// <param name="lines">The number of lines to calculate the base amount for.</param>
        /// <returns>The calculated base amount as a <see cref="decimal"/>.</returns>
        /// <remarks>
        /// If the number of lines is less than the minimum allowed, the minimum value is used. 
        /// If the number of lines exceeds the maximum allowed, the maximum value is used.
        /// The base amount is calculated by dividing the adjusted number of lines by a constant divisor.
        /// </remarks>
        public decimal CalculateBaseAmount(int lines)
        {
            if (lines < StatementPrinterConstants.MINIMUM_LINES)
                lines = StatementPrinterConstants.MINIMUM_LINES;
            if (lines > StatementPrinterConstants.MAXIMUM_LINES)
                lines = StatementPrinterConstants.MAXIMUM_LINES;

            return lines / StatementPrinterConstants.DIVIDER_PER_LINE;
        }
    }
}
