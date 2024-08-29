namespace TheatricalPlayersRefactoringKata.Communication.Request;

public class InvoiceRequest
{
    public string IvoiceNumber { get; set; } = string.Empty;
    public DateTime DateProcessing { get; set; }
    public decimal TotalValue { get; set; }
    public string Status { get; set; } = string.Empty;
}
