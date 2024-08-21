using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StatementPrintApi.Entities
{
    public class Performance
    {
        public int Id { get; set; }
        public int Audience { get; set; }

        public int PlayId { get; set; }
        public Play? Play { get; set; }

        public int InvoiceId { get; set; }
        public Invoice? Invoice { get; set; }
    }
}