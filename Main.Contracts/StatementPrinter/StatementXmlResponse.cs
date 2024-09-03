using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Main.Contracts.StatementPrinter
{
    public class Statement
    {
        public string Customer { get; set; }
        public List<Item> Items { get; set; }
        public decimal AmountOwed { get; set; }
        public int EarnedCredits { get; set; }
    }

    public class Item
    {
        public decimal AmountOwed { get; set; }
        public int EarnedCredits { get; set; }
        public int Seats { get; set; }
    }

}
