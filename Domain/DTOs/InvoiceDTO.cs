using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public  class InvoiceDTO
    {
        public int Id { get; set; }
        public string Customer { get; set; } = string.Empty;
        public List<PerformanceDTO> Performances { get; set; }


    }
}
