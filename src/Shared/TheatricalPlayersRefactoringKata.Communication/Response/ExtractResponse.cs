namespace TheatricalPlayersRefactoringKata.Communication.Response;

public class ExtractResponse
{
    public int TotalInvoices { get; set; }
    public decimal ValueTotal { get; set; }
    public List<InvoiceResponse> Invoices { get; set; } = default!;
}
