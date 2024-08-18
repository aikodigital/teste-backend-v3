using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheatricalPlayersRefactoringKata.DTOs
{
    public class PerformanceSummaryDto
    {
        public string PlayName { get; set; }
        public decimal Amount { get; set; }
        public int Audience { get; set; }
    }
}
