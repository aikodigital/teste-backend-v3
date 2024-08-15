using TheatricalPlayersRefactoringKata.Domain.Entities;


namespace TheatricalPlayersRefactoringKata.Communication.Requests;

public class RequestInvoice
{
    public string Customer { get; set; } = string.Empty;
    public List<Performance> Performances { get; set; } = new();
}
