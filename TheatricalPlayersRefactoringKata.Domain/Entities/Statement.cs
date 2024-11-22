using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheatricalPlayersRefactoringKata.Domain.Entities
{
    public class Statement
    {
        public Statement() { }

        public Statement(string customer)
        {
            Customer = customer;
        }
        public int Id { get; set; }
        public string Customer {  get; set; }
        public int TotalCredits { get; set; }
        public decimal TotalCost { get; set; }

        public List<Performance> Performances { get; set; } = new List<Performance>();
    }
}
