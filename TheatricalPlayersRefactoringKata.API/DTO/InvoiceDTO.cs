namespace TheatricalPlayersRefactoringKata.API.DTO
{
    public class InvoiceDTO
    {
        public string Customer { get; set; }
        public List<PerformanceDTO> Performances { get; set; }

        public InvoiceDTO() { }
    }
}
