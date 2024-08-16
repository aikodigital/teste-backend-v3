using TheatricalPlayersRefactoringKata.Domain.Enums;

namespace TheatricalPlayersRefactoringKata.Communication.Requests;
public class RequestPlay
{
    public string Name { get; set; } = string.Empty;
    public int Lines {  get; set; }

    public PlayTypes Type { get; set; }
}
