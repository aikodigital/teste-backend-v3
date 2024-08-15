using CashFlow.Communication.Enums;

namespace TheatricalPlayersRefactoringKata.Communication.Requests;

public class RequestExpenses
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime Date { get; set; }

    public decimal Amount { get; set; }

    public PaymentTypes PaymentType { get; set; }
}
