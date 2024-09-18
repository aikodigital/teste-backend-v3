namespace TheatricalPlayersRefactoringWebAPI.DTO
{
    public class InvoiceRequest
    {
        public string Customer { get; set; }
        public List<PerformanceRequest> Performances { get; set; }
    }
}
