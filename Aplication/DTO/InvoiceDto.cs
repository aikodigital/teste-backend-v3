namespace Aplication.DTO
{
    public class InvoiceDto
    {
        public string Customer { get; set; }
        public List<PerformanceDto> Performances { get; set; }

        public InvoiceDto(string customer, List<PerformanceDto> performance)
        {
            Customer = customer;
            Performances = performance;
        }

    }
}
