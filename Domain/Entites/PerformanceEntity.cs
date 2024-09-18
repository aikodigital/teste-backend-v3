using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entites
{
    public class PerformanceEntity
    {
        public int Id { get; set; }
        public int PlayId { get; set; }
        public PlayEntity Play { get; set; }
        public int InvoiceId { get; set; }
        public InvoiceEntity Invoice { get; set; }

        public int Audience { get; set; }
    }
}
