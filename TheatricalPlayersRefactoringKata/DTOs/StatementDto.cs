using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheatricalPlayersRefactoringKata.DTOs
{
    public class StatementDto
    {
        public string Customer { get; set; }
        public List<PerformanceSummaryDto> Performances { get; set; }
        public decimal TotalAmount { get; set; }
        public int TotalCredits { get; set; }
    }
}
