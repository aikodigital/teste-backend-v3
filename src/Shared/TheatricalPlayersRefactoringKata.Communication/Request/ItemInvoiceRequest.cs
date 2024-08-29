namespace TheatricalPlayersRefactoringKata.Communication.Request;

public class ItemInvoiceRequest
{
    public string Description { get; set; } = string.Empty;
    public decimal UnitPrice { get; set; }
    public int Quantity { get; set; }
}
