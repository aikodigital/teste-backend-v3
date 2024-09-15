using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Application.Interfaces;
using TheatricalPlayersRefactoringKata.Domain;
using TheatricalPlayersRefactoringKata.Domain.Entities;

namespace TheatricalPlayersRefactoringKata.Application.Strategy
{
    public class InvoicePrintText : IInvoicePrint
    {
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
