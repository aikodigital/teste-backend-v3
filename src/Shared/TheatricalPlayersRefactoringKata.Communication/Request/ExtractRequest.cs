namespace TheatricalPlayersRefactoringKata.Communication.Request;

public class ExtractRequest
{
    public DateTime StartDate { get; set; }
    public DateTime DataFim { get; set; }
    public string? InvoiceType { get; set; } 
}
