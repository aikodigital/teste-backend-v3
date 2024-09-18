using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entites
{
    public  class InvoiceEntity
    {
        public int Id { get; set; }
        public string Customer { get; set; } = string.Empty;
        public List<PerformanceEntity> Performances { get; set; }


    }
}
