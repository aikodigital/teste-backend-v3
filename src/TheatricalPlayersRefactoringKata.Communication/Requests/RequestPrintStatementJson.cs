using TheatricalPlayersRefactoringKata.Domain.Entities;
using TheatricalPlayersRefactoringKata.Domain.ValueObjects;

namespace TheatricalPlayersRefactoringKata.Communication.Requests
{
    public class RequestPrintStatementJson
    {
        public string FormatFile { get; set; } = string.Empty;
        public Dictionary<string, Play> Plays { get; set; } = new Dictionary<string, Play>();
        public Invoice Invoice { get; set; } = new Invoice(string.Empty, new List<Performance>());
    }
}
