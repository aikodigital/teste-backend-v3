using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheatricalPlayersRefactoringKata.XmlModels
{
    public class Statement
    {
        public string Customer { get; set; }
        public List<Item> Items { get; set; } = new List<Item>();
        public decimal AmountOwed { get; set; }
        public int EarnedCredits { get; set; }
    }
}
