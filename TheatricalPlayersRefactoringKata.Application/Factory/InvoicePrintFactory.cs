using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Application.Exceptions;
using TheatricalPlayersRefactoringKata.Application.Interfaces;
using TheatricalPlayersRefactoringKata.Application.Strategy;
using TheatricalPlayersRefactoringKata.Domain.Entities;
using TheatricalPlayersRefactoringKata.Domain.Enum;

namespace TheatricalPlayersRefactoringKata.Application.Factory
{
    /// <summary>
    /// A factory class for creating instances of <see cref="IInvoicePrint"/> based on the specified <see cref="PrintType"/>.
    /// </summary>
    public class InvoicePrintFactory : IInvoicePrintFactory
    {
        /// <summary>
        /// Creates an instance of <see cref="IInvoicePrint"/> based on the provided <see cref="PrintType"/>.
        /// </summary>
        /// <param name="type">The type of print to be created. This can be either <see cref="PrintType.XML"/> or <see cref="PrintType.Text"/>.</param>
        /// <returns>An instance of <see cref="IInvoicePrint"/> that corresponds to the specified print type.</returns>
        /// <exception cref="InvalidPrintTypeException">Thrown when an unknown <see cref="PrintType"/> is provided.</exception>
        public IInvoicePrint GetPrintType(PrintType type)
        {
            switch (type)
            {
                case PrintType.XML:
                    return new InvoicePrintXML();
                case PrintType.Text:
                    return new InvoicePrintText();
                default:
                    throw new InvalidPrintTypeException("unknown Print type: " + type.ToString());
            }
        }

        /// <summary>
        /// Determines the <see cref="PrintType"/> based on the provided string representation of the print type.
        /// </summary>
        /// <param name="printTypeRequest">A string representing the print type. It should be either "XML" or "TEXT".</param>
        /// <returns>The <see cref="PrintType"/> corresponding to the provided string.</returns>
        /// <exception cref="InvalidPrintTypeException">Thrown when the provided string does not match any known print type.</exception>
        public PrintType DeterminePrintType(string printTypeRequest)
        {
            return printTypeRequest.ToUpper() switch
            {
                "XML" => PrintType.XML,
                "TEXT" => PrintType.Text,
                _ => throw new InvalidPrintTypeException(printTypeRequest),
            };
        }
    }
}
