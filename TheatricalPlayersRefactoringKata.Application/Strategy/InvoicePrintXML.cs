using System;
using System.Xml.Serialization;
using TheatricalPlayersRefactoringKata.Application.Extensions;
using TheatricalPlayersRefactoringKata.Application.Interfaces;
using TheatricalPlayersRefactoringKata.Domain.Entities;

namespace TheatricalPlayersRefactoringKata.Application.Strategy
{
    /// <summary>
    /// Provides functionality to print invoices in XML format.
    /// Implements the <see cref="IInvoicePrint"/> interface.
    /// </summary>
    public class InvoicePrintXML : IInvoicePrint
    {
        /// <summary>
        /// Formats an invoice statement as an XML string.
        /// Adds a Byte Order Mark (BOM) to the beginning of the XML string to indicate UTF-8 encoding.
        /// </summary>
        /// <param name="invoicePrint">The invoice print statement to format.</param>
        /// <returns>A formatted XML representation of the invoice, including BOM for UTF-8 encoding.</returns>
        public string Print(InvoicePrint.Statement invoicePrint)
        {
            return $"\uFEFF{invoicePrint.GenerateXmlAsString()}";
        }
    }
}
