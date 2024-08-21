using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StatementPrintApi.Entities
{
    public class Play
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Type { get; set; }
        public int Lines { get; set; }

        public ICollection<Performance>? Performances { get; set; }
    }
}