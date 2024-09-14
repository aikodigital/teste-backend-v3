namespace TheatricalPlayersRefactoringKata.Application.Request
{
    public class PrinterWorkerRequest
    {
        public InvoiceWorkerRequest Invoice { get; set; }
        public string Type { get; set; }
    }

    public class InvoiceWorkerRequest
    {
        public string Customer { get; set; }
        public List<PerformanceWorkerRequest> Performances { get; set; }
    }

    public class PerformanceWorkerRequest
    {
        public int Id { get; set; }
        public string PlayId { get; set; }
        public int Audience { get; set; }
    }
}
