namespace TheatricalPlayersRefactoringWebAPI.DTO
{
    public class StatementRequest
    {
        public InvoiceRequest Invoice { get; set; }
        public List<PlayRequest> Plays { get; set; }
    }
}
