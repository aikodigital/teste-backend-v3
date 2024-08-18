using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheatricalPlayersRefactoringKata.Models
{
    public class Statement
    {
        public string Customer { get; set; }
        public List<PerformanceSummary> Performances { get; set; }
        public decimal TotalAmount { get; set; }
        public int TotalCredits { get; set; }

        public Statement()
        {
            Performances = new List<PerformanceSummary>();
        }
    }
}
