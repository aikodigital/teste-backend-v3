using TheatricalPlayersRefactoringKata.Domain.Enums;

namespace TheatricalPlayersRefactoringKata.Communication.Responses;
public class ResponsePlay
{
    public string Name { get; set; } = string.Empty;
    public int Lines { get; set; }
    public PlayTypes Type { get; set; }
}
