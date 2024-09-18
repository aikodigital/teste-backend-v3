namespace TheatricalPlayersRefactoringKata.Application.Interfaces
{
    /// <summary>
    /// Defines a contract for calculating the base amount based on the number of lines.
    /// </summary>
    public interface ICalculateBaseAmountPerLine
    {
        /// <summary>
        /// Calculates the base amount for a given number of lines.
        /// </summary>
        /// <param name="lines">The number of lines to use for the calculation.</param>
        /// <returns>The calculated base amount based on the number of lines.</returns>
        decimal CalculateBaseAmount(int lines);
    }
}
