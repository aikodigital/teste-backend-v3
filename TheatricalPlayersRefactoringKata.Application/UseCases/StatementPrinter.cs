using TheatricalPlayersRefactoringKata.Domain;

namespace TheatricalPlayersRefactoringKata.Application;

public class StatementPrinter
{
    public string Print(Invoice invoice)
    {
        return invoice.PrintInvoiceStatement();
    }
}
