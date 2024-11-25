using System.Text.Json.Serialization;

namespace Aplication.DTO
{
    public class InvoiceDto
    {
        public Guid Id { get; set; }
        public string Customer { get; set; }
        public List<PerformanceDto> Performances { get; set; }
    }
}
