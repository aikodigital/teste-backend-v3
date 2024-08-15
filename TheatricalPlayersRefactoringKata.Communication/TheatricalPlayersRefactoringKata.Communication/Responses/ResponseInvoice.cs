using CashFlow.Communication.Enums;

namespace TheatricalPlayersRefactoringKata.Communication.Responses;

public class ResponseInvoice
{
    public long Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTime Date { get; set; }
    public decimal Amount { get; set; }
    public PaymentTypes PaymentType { get; set; }
}
