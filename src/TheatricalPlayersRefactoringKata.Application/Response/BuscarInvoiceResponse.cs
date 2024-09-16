namespace TheatricalPlayersRefactoringKata.Application.Response
{
    public class BuscarInvoiceResponse
    {
        public int Id { get; set; }
        public string Customer { get; set; }
        public List<BuscarInvoicePerformanceResponse> Performances { get; set; }
    }

    public class BuscarInvoicePerformanceResponse
    {
        public int Id { get; set; }
        public string PlayId { get; set; }
        public int Audience { get; set; }
    }
}
