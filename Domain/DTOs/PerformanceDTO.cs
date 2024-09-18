using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public class PerformanceDTO
    {
        public int Id { get; set; }
        public PlayDTO Play { get; set; }
        public InvoiceDTO Invoice { get; set; }
        public int Audience { get; set; }
    }
}
