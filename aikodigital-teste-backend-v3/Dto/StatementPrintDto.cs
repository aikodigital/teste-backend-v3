using aikodigital_teste_backend_v3.Models;

namespace aikodigital_teste_backend_v3.Dto
{
    public class StatementPrintDto
    {
        public required Invoice invoice { get; set; }
        public required Dictionary<string, Play> plays { get; set; }
        public required string format { get; set; }
    }
}
