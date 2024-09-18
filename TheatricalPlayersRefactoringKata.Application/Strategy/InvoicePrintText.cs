using System;
using System.Globalization;
using System.Linq;
using TheatricalPlayersRefactoringKata.Application.Interfaces;
using TheatricalPlayersRefactoringKata.Domain.Entities;

namespace TheatricalPlayersRefactoringKata.Application.Strategy
{
    /// <summary>
    /// Provides functionality to print invoices in a text format.
    /// Implements the <see cref="IInvoicePrint"/> interface.
    /// </summary>
    public class InvoicePrintText : IInvoicePrint
    {
        /// <summary>
        /// Formats an invoice statement as a plain text string.
        /// </summary>
        /// <param name="invoicePrint">The invoice print statement to format.</param>
        /// <returns>A formatted text representation of the invoice.</returns>
        public string Print(InvoicePrint.Statement invoicePrint)
        {
            CultureInfo cultureInfo = new CultureInfo("en-US");

            var invoiceText = string.Format("Statement for {0}\n", invoicePrint.Customer);
            foreach (var itemInvoice in invoicePrint.Items.Item)
            {
                invoiceText += string.Format(cultureInfo, "  {0}: {1:C} ({2} seats)\n", itemInvoice.Name, itemInvoice.AmountOwed, itemInvoice.Seats);
            }
            invoiceText += string.Format(cultureInfo, "Amount owed is {0:C}\n", invoicePrint.AmountOwed);
            invoiceText += string.Format("You earned {0} credits\n", invoicePrint.EarnedCredits);
            return invoiceText;
        }
    }
}
