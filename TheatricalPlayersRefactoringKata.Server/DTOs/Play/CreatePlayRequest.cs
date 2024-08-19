using TheatricalPlayersRefactoringKata.Server.Database.DTOs.Play;

namespace TheatricalPlayersRefactoringKata.Server.DTOs.Play
{
    public class CreatePlayRequest
    {
        public required PlayDTO Play { get; set; }
    }
}