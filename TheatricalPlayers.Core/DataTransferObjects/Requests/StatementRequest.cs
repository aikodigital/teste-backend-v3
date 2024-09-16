using TheatricalPlayers.Core.DataTransferObjects.StatementDTOs;

namespace TheatricalPlayers.Core.DataTransferObjects.Requests;

public class StatementRequest
{
    public Invoice Invoice { get; set; }
    public List<Play> Plays { get; set; }
}