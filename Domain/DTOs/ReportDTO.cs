using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public class ReportDTO
    {
        public int Id { get; set; }
        public string Statement { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public int Seats { get; set; }
        public decimal Amount { get; set; }
        public int Credits { get; set; }

    }
}
