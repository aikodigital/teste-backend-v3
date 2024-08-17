using System.Collections.Generic;

namespace TheatricalPlayersRefactoringKata.DTOs
{
    public class InvoiceDto
    {
        public string Customer { get; set; }
        public List<int> PerformanceIds { get; set; }
    }
}
