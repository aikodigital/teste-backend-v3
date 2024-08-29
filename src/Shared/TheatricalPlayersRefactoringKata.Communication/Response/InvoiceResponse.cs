namespace TheatricalPlayersRefactoringKata.Communication.Response;

public class InvoiceResponse
{

    public Guid Id { get; set; }
    public DateTime DataFatura { get; set; }
    public string Customer { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public string? ErrorMessage { get; set; }
    public List<ItemInvoiceResponse>? Itens { get; set; }
}
