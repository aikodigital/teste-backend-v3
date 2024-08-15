using TheatricalPlayersRefactoringKata.Domain.Entities;

namespace TheatricalPlayersRefactoringKata.Communication.Responses;

public class ResponseInvoice
{
    public long Id { get; set; }
    public string Customer { get; set; } = string.Empty;
    public List<Performance> Performances { get; set; } = new();
}
