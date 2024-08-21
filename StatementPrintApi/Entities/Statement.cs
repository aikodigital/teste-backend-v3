using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StatementPrintApi.Entities
{
    public class Statement
    {
        public int Id { get; set; }
        public string Customer { get; set; }
        public List<Performance> Performances { get; set; } = new List<Performance>();
        public decimal TotalAmount { get; set; }
        public int TotalCredits { get; set; }
    }
}
