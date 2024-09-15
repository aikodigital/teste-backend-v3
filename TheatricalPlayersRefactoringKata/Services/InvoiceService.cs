using System.Collections.Generic;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Models;
using TheatricalPlayersRefactoringKata.Services;

public class InvoiceService
{
    public string ProcessInvoice(Invoice invoice, Dictionary<string, Play> plays, StatementPrinter printer)
    {
        return printer.Print(invoice, plays);
    }

    public async Task<string> ProcessInvoiceAsync(Invoice invoice, Dictionary<string, Play> plays, StatementPrinter printer)
    {
        return await Task.Run(() => printer.Print(invoice, plays));
    }
}
