using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StatementPrintApi.Entities
{
    public class Invoice
    {
        public int Id { get; set; }
        public string? Customer { get; set; }

        public ICollection<Performance>? Performances { get; set; }
    }
}