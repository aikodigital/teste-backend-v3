using TheatricalPlayersRefactoringKata.Domain.Enum;

namespace TheatricalPlayersRefactoringKata.Application.Interfaces
{
    /// <summary>
    /// Defines a contract for creating invoice print types and determining print types based on requests.
    /// </summary>
    public interface IInvoicePrintFactory
    {
        /// <summary>
        /// Creates an instance of <see cref="IInvoicePrint"/> based on the specified print type.
        /// </summary>
        /// <param name="type">The type of print format to create.</param>
        /// <returns>An instance of <see cref="IInvoicePrint"/> corresponding to the specified print type.</returns>
        IInvoicePrint GetPrintType(PrintType type);

        /// <summary>
        /// Determines the print type based on the given print type request.
        /// </summary>
        /// <param name="printTypeRequest">The request specifying the desired print type as a string.</param>
        /// <returns>The corresponding <see cref="PrintType"/> based on the print type request.</returns>
        PrintType DeterminePrintType(string printTypeRequest);
    }
}
