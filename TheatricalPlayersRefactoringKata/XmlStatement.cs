using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheatricalPlayersRefactoringKata
{
    public class Statement
    {
        public string Customer { get; set; }
        public List<StatementItem> Items { get; set; }
        public decimal AmountOwed { get; set; }
        public int EarnedCredits { get; set; }
    }

    public class StatementItem
    {
        public decimal AmountOwed { get; set; }
        public int EarnedCredits { get; set; }
        public int Seats { get; set; }
    }
}
