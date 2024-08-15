
using TheatricalPlayersRefactoringKata.Domain.Enums;

namespace TheatricalPlayersRefactoringKata.Communication.Requests;

public class RequestInvoice
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime Date { get; set; }

    public decimal Amount { get; set; }

    public PlayTypes PlayType { get; set; }
}
