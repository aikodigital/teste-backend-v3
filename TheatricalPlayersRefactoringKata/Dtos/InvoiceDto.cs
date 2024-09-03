using System.Collections.Generic;

namespace TheatricalPlayersRefactoringKata.Dtos
{
    public class InvoiceDto
    {
        public string Customer { get; set; }
        public List<PerformanceDto> Performances { get; set; }
    }

}
